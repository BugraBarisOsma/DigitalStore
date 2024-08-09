namespace DigitalStore.Core.DTOs;

public class OrderRequestDTO
{
    public string UserId { get; set; }
    
    public string CouponCode { get; set; } 
    public List<OrderItemDTO> OrderItems { get; set; }
}