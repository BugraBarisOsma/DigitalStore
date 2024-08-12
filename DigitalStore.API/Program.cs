
using System.Reflection;
using System.Text;
using DigitalStore.API.Middlewares;
using DigitalStore.Business.Helpers;
using DigitalStore.Business.Mapping;
using DigitalStore.Business.Notifications.Abstract;
using DigitalStore.Business.Notifications.Concrete;
using DigitalStore.Business.Services.Abstract;
using DigitalStore.Business.Services.Concrete;
using DigitalStore.Core.DTOs.JWT;
using DigitalStore.Data.Contexts;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Data.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;


var builder = WebApplication.CreateBuilder(args);

// Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
// Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddSingleton<ConnectionFactory>(opt => new ConnectionFactory()
{
    HostName = "localhost",
    Port = 5672,
    UserName = "guest",
    Password = "guest",

});
//AutoMapper
builder.Services.AddAutoMapper(typeof(CategoryProfile).Assembly);

//Hangfire
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new PostgreSqlStorageOptions
    {
        PrepareSchemaIfNecessary = true,
        InvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.FromMilliseconds(50),
        UseNativeDatabaseTransactions = true,
        DistributedLockTimeout = TimeSpan.FromDays(30),
        
    }));
builder.Services.AddHangfireServer();

//FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryRequestValidation>();

// JWT
builder.Services.AddScoped<JwtGenerator>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id,jwtSecurityScheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement{{jwtSecurityScheme,Array.Empty<string>()}});
    
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "DigitalStore API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    setup.IncludeXmlComments(xmlPath);
});

// DbContext
builder.Services.AddDbContext<AppDbContext>(opt=>{
    opt.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]);
    opt.LogTo(Console.WriteLine, LogLevel.Information);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DigitalStore API V1");
    });
}

app.UseHangfireDashboard("/hangfire");
app.UseHangfireServer();

var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();
recurringJobManager.AddOrUpdate<INotificationService>("send-queued-emails", 
    service => service.SendQueuedEmails(), "*/5 * * * * *");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.Run();

