using System.Security.Claims;
using DigitalStore.Business.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PointsController : ControllerBase
{
    private readonly IUserService _userService;

    public PointsController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserPoints()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var points = await _userService.GetUserPointsAsync(userId);
        return Ok(points);
    }
}