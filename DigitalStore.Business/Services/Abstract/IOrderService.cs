
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalStore.Core.DTOs;

namespace DigitalStore.Business.Services.Abstract;


public interface IOrderService
{
    
    Task<List<OrderResponseDTO>> GetActiveOrdersAsync();
    Task<List<OrderResponseDTO>> GetOrderHistoryAsync();
    Task<OrderResponseDTO> GetOrderDetailsAsync(Guid id);
}