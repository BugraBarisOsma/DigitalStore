using DigitalStore.Core.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalStore.Data.Contexts;

public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategory> CategoryProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Coupon> Coupons { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        base.OnModelCreating(builder);
        base.OnModelCreating(builder);

        // IdentityUser özelliklerini yoksaymak icin bunlar eklendi
        builder.Entity<User>().Ignore(u => u.UserName);
        builder.Entity<User>().Ignore(u => u.EmailConfirmed);
        builder.Entity<User>().Ignore(u => u.PasswordHash);
        builder.Entity<User>().Ignore(u => u.PhoneNumber);
        builder.Entity<User>().Ignore(u => u.PhoneNumberConfirmed);
        builder.Entity<User>().Ignore(u => u.TwoFactorEnabled);
        builder.Entity<User>().Ignore(u => u.LockoutEnd);
        builder.Entity<User>().Ignore(u => u.LockoutEnabled);
        builder.Entity<User>().Ignore(u => u.AccessFailedCount);

        var roles = new[]
        {
            
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            },

            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);
        
        var adminUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            Username = "admin",
            Surname = "admin",
            Password = "admin123",
            Role = "Admin",
            Email = "admin@example.com",
            NormalizedUserName = "ADMIN",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            isActive = true
        };
        
        builder.Entity<User>().HasData(adminUser);
        
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = adminUser.Id,
                RoleId = roles.First(r => r.Name == "Admin").Id
            }
        );

    }
}