using AutoMapper;
using DigitalStore.Core.DTOs;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Business.Services.Abstract;
using DigitalStore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DigitalStore.Business.Services.Concrete;


public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork; 
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
     
    }
    
    public async Task<List<OrderResponseDTO>> GetActiveOrdersAsync()
    {
        var activeOrders = await _unitOfWork.GetRepository<Order>()
            .GetAllByFilterAsync(o => o.IsActive,include:q=>q.Include(o=>o.OrderDetails)); 
        return _mapper.Map<List<OrderResponseDTO>>(activeOrders);
    }
    public async Task<Order> CreateOrderForUserAsync(string userId)
    {
        var order = new Order
        {
            UserId = userId,
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            OrderDetails = new List<OrderDetail>(),
            TotalAmount = 0,
            Id = Guid.NewGuid(),
            PointsUsed = 0,
            CouponAmount = 0,
            CouponCode = string.Empty,
        };

        await _unitOfWork.GetRepository<Order>().AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return order;
    }


    public async Task<List<OrderResponseDTO>> GetOrderHistoryAsync()
    {
        var orderHistory = await _unitOfWork.GetRepository<Order>()
            .GetAllByFilterAsync(o => !o.IsActive,include:q=>q.Include(o=>o.OrderDetails)); 
        return _mapper.Map<List<OrderResponseDTO>>(orderHistory);
    }

    public async Task<OrderResponseDTO> GetOrderDetailsAsync(Guid id)
    {
        var userId = id.ToString();
        var order = await _unitOfWork.GetRepository<Order>()
            .GetByFilterAsync(o => o.UserId == userId && o.IsActive, include: q => q.Include(o => o.OrderDetails));
        if (order == null)
        {
            throw new Exception("Order not found");
        }

        var orderDetails = _mapper.Map<OrderResponseDTO>(order);
        return orderDetails;
    }
}
