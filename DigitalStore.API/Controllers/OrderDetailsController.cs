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

    [HttpGet]
    public async Task<IActionResult> GetAllOrderDetails()
    {
        var orderDetails = await _orderDetailService.GetAllOrderDetailsAsync();
        return Ok(orderDetails);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderDetail([FromBody] OrderDetail orderDetail)
    {
        await _orderDetailService.CreateOrderDetailAsync(orderDetail);
        return CreatedAtAction(nameof(GetOrderDetail), new { id = orderDetail.Id }, orderDetail);
    }

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderDetail(Guid id)
    {
        await _orderDetailService.DeleteOrderDetailAsync(id);
        return NoContent();
    }
}
