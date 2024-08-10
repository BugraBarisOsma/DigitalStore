using AutoMapper;
using DigitalStore.Business.Services.Abstract;
using DigitalStore.Core.DTOs;
using DigitalStore.Data.UnitOfWork;

namespace DigitalStore.Business.Services.Concrete;

public class OrderDetailService : IOrderDetailService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    

    public OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDetail> GetOrderDetailByIdAsync(Guid id)
    {
        return await _unitOfWork.GetRepository<OrderDetail>().GetByIdAsync(id);
    }

    public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
    {
        return await _unitOfWork.GetRepository<OrderDetail>().GetAllAsync();
    }

    public async Task CreateOrderDetailAsync(OrderDetail orderDetail)
    {
        await _unitOfWork.GetRepository<OrderDetail>().AddAsync(orderDetail);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
    {
        _unitOfWork.GetRepository<OrderDetail>().UpdateAsync(orderDetail);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteOrderDetailAsync(Guid id)
    {
        var orderDetail = await _unitOfWork.GetRepository<OrderDetail>().GetByIdAsync(id);
        if (orderDetail != null)
        {
            orderDetail.IsActive = false;
            _unitOfWork.GetRepository<OrderDetail>().UpdateAsync(orderDetail);
            await _unitOfWork.SaveChangesAsync();
        }
    }
    public async Task<Order> AddProductToOrderAsync(Guid orderId, OrderItemDTO orderItemDto)
    {
        var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(orderId);
        var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(orderItemDto.ProductId);
        
        if (order == null)
            throw new Exception("Order not found");
        if (product.Stock < orderItemDto.Quantity)
            throw new Exception("Insufficient stock");
        if (order.OrderDetails == null)
            order.OrderDetails = new List<OrderDetail>();
        
        
        var existingOrderDetail = order.OrderDetails.FirstOrDefault(od => od.ProductId == orderItemDto.ProductId);
        if (existingOrderDetail != null){
            existingOrderDetail.Quantity += orderItemDto.Quantity;
        }
        else
        {
            var orderDetail = new OrderDetail
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = orderItemDto.ProductId,
                Quantity = orderItemDto.Quantity,
                Price = product.Price
            };
            order.OrderDetails.Add(orderDetail);
            await _unitOfWork.GetRepository<OrderDetail>().AddAsync(orderDetail);
        }
        product.Stock -= orderItemDto.Quantity;
        order.TotalAmount += order.OrderDetails.Sum(od => od.Price * od.Quantity);
        await _unitOfWork.SaveChangesAsync();
        
        return order;
    }
    

}
