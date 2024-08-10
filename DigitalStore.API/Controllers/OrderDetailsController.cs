using System.Text.Json;
using System.Text.Json.Serialization;
using DigitalStore.Business.Services.Abstract;
using DigitalStore.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailController : ControllerBase
{
    private readonly IOrderDetailService _orderDetailService;

    public OrderDetailController(IOrderDetailService orderDetailService)
    {
        _orderDetailService = orderDetailService;
    }
    /// <summary>
    /// Add a product to your order
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="404">Order Not Found</response>
    [HttpPost("add-product/{orderId}")]
    public async Task<IActionResult> AddProductToOrder(Guid orderId, OrderItemDTO orderItemDto)
    {
        try
        {
            var orderResponse = await _orderDetailService.AddProductToOrderAsync(orderId, orderItemDto);
            var options = new JsonSerializerOptions
            {
                MaxDepth = 64, 
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(orderResponse, options);
            return Ok(json);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    /// <summary>
    /// Get order details
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="404">Order Not Found</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetail(Guid id)
    {
        var orderDetail = await _orderDetailService.GetOrderDetailByIdAsync(id);
        if (orderDetail == null)
        {
            return NotFound();
        }
        return Ok(orderDetail);
    }
    /// <summary>
    /// Get all order details
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="404">Order Not Found</response>
    [HttpGet]
    public async Task<IActionResult> GetAllOrderDetails()
    {
        var orderDetails = await _orderDetailService.GetAllOrderDetailsAsync();
        return Ok(orderDetails);
    }

    /// <summary>
    /// Update a order detail
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="404">Order Not Found</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderDetail(Guid id, [FromBody] OrderDetail orderDetail)
    {
        if (id != orderDetail.Id)
        {
            return BadRequest();
        }
        await _orderDetailService.UpdateOrderDetailAsync(orderDetail);
        return NoContent();
    }
    /// <summary>
    /// delete a order detail
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="404">Order Not Found</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderDetail(Guid id)
    {
        await _orderDetailService.DeleteOrderDetailAsync(id);
        return NoContent();
    }
}
