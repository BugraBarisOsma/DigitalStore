using DigitalStore.Business.Services.Abstract;
using DigitalStore.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class CouponsController : ControllerBase
{
    private readonly ICouponService _couponService;

    public CouponsController(ICouponService couponService)
    {
        _couponService = couponService;
    }
    /// <summary>
    /// Create a coupon
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost]
    public async Task<IActionResult> CreateCoupon(CouponRequestDTO couponDto)
    {
        await _couponService.CreateCouponAsync(couponDto);
        return Ok();
    }
    /// <summary>
    /// Get all coupons
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet]
    [Authorize(Roles = "Admin,Customer")] 
    public async Task<IActionResult> GetCoupons()
    {
        var coupons = await _couponService.GetCouponsAsync();
        return Ok(coupons);
    }
    /// <summary>
    /// Delete a coupon
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoupon(Guid id)
    {
        await _couponService.DeleteCouponAsync(id);
        return Ok();
    }
}