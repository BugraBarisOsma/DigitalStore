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

    [HttpPost]
    public async Task<IActionResult> CreateCoupon(CouponRequestDTO couponDto)
    {
        await _couponService.CreateCouponAsync(couponDto);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetCoupons()
    {
        var coupons = await _couponService.GetCouponsAsync();
        return Ok(coupons);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoupon(Guid id)
    {
        await _couponService.DeleteCouponAsync(id);
        return Ok();
    }
}