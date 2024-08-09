using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DigitalStore.Core.DTOs;
using DigitalStore.Business.Services.Abstract; // Make sure to include your service interface

namespace DigitalStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }


        [HttpPost("{orderId}")]
        public async Task<IActionResult> CheckoutAsync([FromRoute]Guid orderId, [FromQuery] string couponCode = null, [FromQuery] decimal pointsToUse = 0)
        {
            try
            {
                var result = await _checkoutService.CheckoutAsync(orderId, couponCode, pointsToUse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException.Message });
            }
        }
    }
}