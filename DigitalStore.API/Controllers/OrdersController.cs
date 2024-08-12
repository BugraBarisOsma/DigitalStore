using DigitalStore.Business.Services.Abstract;
using DigitalStore.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    /// <summary>
    /// Get all active orders
    /// </summary>
    /// <response code="200">Returns orders</response>
    /// <response code="404">if orders not found</response>
    [HttpGet("active")]
    public async Task<IActionResult> GetActiveOrders()
    {
        var orders = await _orderService.GetActiveOrdersAsync();
        return Ok(orders);
    }

    /// <summary>
    /// Get order history
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="404">Order/Orders Not Found</response>
    [HttpGet("history")]
    public async Task<IActionResult> GetOrderHistory()
    {
        var orders = await _orderService.GetOrderHistoryAsync();
        return Ok(orders);
    }
    /// <summary>
    /// Get your order's details
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="404">Order Not Found</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetails(Guid id)
    {
        var orderDetails = await _orderService.GetOrderDetailsAsync(id);
        return Ok(orderDetails);
    }
}