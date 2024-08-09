using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DigitalStore.Business.Helpers;
using DigitalStore.Core.DTOs.JWT;
using DigitalStore.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using LoginRequest = DigitalStore.Core.DTOs.LoginRequest;

namespace DigitalStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtGenerator _jwtGenerator;

    public AuthController(IConfiguration configuration, IUnitOfWork unitOfWork, JwtGenerator jwtGenerator)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _jwtGenerator = jwtGenerator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _unitOfWork.GetRepository<User>()
            .GetByFilterAsync(x => x.Username == request.Username && x.Password == request.Password);
        
        if (user == null)
        {
            return Unauthorized("Invalid credentials");
        }

        var token = _jwtGenerator.GenerateToken(user);
        return Ok(new { Token = token });
    }
}