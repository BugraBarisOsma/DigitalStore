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
            .GetAllByFilterAsync(o => o.IsActive); 
        return _mapper.Map<List<OrderResponseDTO>>(activeOrders);
    }


    public async Task<List<OrderResponseDTO>> GetOrderHistoryAsync()
    {
        var orderHistory = await _unitOfWork.GetRepository<Order>()
            .GetAllByFilterAsync(o => !o.IsActive); 
        return _mapper.Map<List<OrderResponseDTO>>(orderHistory);
    }

    public async Task<OrderResponseDTO> GetOrderDetailsAsync(Guid id)
    {
        var orders = await _unitOfWork.GetRepository<Order>()
            .GetAllByFilterAsync(o => o.Id == id && o.IsActive, include: q => q.Include(o => o.OrderDetails));
        var order = orders.FirstOrDefault();
        if (order == null)
        {
            throw new Exception("Order not found");
        }

        var orderDetails = _mapper.Map<OrderResponseDTO>(order);
        return orderDetails;
    }
}
