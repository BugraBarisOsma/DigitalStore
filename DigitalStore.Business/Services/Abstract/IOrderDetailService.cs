using DigitalStore.Core.DTOs;

namespace DigitalStore.Business.Services.Abstract;

public interface IOrderDetailService
{
    Task<OrderDetail> GetOrderDetailByIdAsync(Guid id);
    Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
    Task CreateOrderDetailAsync(OrderDetail orderDetail);
    Task UpdateOrderDetailAsync(OrderDetailRequestDTO orderDetail);
    Task DeleteOrderDetailAsync(Guid id);
    Task<Order> AddProductToOrderAsync(Guid orderId, OrderItemDTO orderItemDto);
}