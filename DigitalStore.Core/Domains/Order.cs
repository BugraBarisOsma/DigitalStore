using Microsoft.AspNetCore.Identity;

public class Order 
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public decimal TotalAmount { get; set; }
    public string? CouponCode { get; set; }
    public decimal CouponAmount { get; set; }
    public decimal PointsUsed { get; set; }
    public bool IsActive { get; set; } 
    public List<OrderDetail> OrderDetails { get; set; }
}