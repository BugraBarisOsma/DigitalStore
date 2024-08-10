using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
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
    private readonly IOrderService _orderService;

    public UsersController(IUserService userService,IOrderService orderService)
    {
        _userService = userService;
        _orderService= orderService;
    }
    /// <summary>
    /// Create a user
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRequestDTO userDto)
    {
        var result = await _userService.RegisterUserAsync(userDto);
        if (result == null)
            return BadRequest();

        var options = new JsonSerializerOptions
        {
            MaxDepth = 64, 
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(result, options);
        _orderService.CreateOrderForUserAsync(result.Id);
        return Ok(json);
    }
    /// <summary>
    /// Update a user
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Unauthorized</response>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(Guid id, UserRequestDTO userDto)
    {
        await _userService.UpdateUserAsync(id, userDto);
        return Ok();
    }
    /// <summary>
    /// Delete a user
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Unauthorized</response>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok();
    }
    /// <summary>
    /// Get points of a user
    /// </summary>
    /// <response code="200">Success</response>

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserPoints(string id)
    {
       
        var points = await _userService.GetUserPointsAsync(id);
        return Ok(points);
    }
}