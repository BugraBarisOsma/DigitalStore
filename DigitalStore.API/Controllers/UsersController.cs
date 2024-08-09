using DigitalStore.Business.Services.Abstract;
using DigitalStore.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRequestDTO userDto)
    {
        var result = await _userService.RegisterUserAsync(userDto);
        if (result == null)
            return BadRequest();

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserRequestDTO userDto)
    {
        var token = await _userService.LoginUserAsync(userDto);
        if (token == null)
            return Unauthorized();

        return Ok(new { Token = token });
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(Guid id, UserRequestDTO userDto)
    {
        await _userService.UpdateUserAsync(id, userDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok();
    }
}