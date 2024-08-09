namespace DigitalStore.Core.DTOs;

public class OrderResponseDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserResponseDTO User { get; set; }
    public decimal TotalAmount { get; set; }
    public string CouponCode { get; set; }
    public decimal CouponAmount { get; set; }
    public decimal PointsUsed { get; set; }
    public List<OrderDetailResponseDTO> OrderDetails { get; set; }
}