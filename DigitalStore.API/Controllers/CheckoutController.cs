using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DigitalStore.Business.Notifications.Abstract;
using DigitalStore.Core.DTOs;
using DigitalStore.Business.Services.Abstract; // Make sure to include your service interface

namespace DigitalStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public CheckoutController(ICheckoutService checkoutService,INotificationService notificationService,IUserService userService)
        {
            _notificationService= notificationService;
            _checkoutService = checkoutService;
            _userService = userService;
        }

        /// <summary>
        /// Make payment
        /// </summary>
        /// <response code="200">Success</response>
        [HttpPost("{orderId}")]
        public async Task<IActionResult> CheckoutAsync([FromRoute]Guid orderId, [FromQuery] string couponCode = null, [FromQuery] decimal pointsToUse = 0)
        {
            try
            {
                var result = await _checkoutService.CheckoutAsync(orderId, couponCode, pointsToUse);
                
                _notificationService.SendEmail("Order Confirmation", result.UserId, "Your order has been confirmed. Thank you for shopping with us.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException.Message });
            }
        }
    }
}