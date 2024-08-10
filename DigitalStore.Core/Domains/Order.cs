using DigitalStore.Core.Domains.Base;
using Microsoft.AspNetCore.Identity;

public class Order : BaseEntity
{
    
    public string UserId { get; set; }
    public User User { get; set; }
    public decimal TotalAmount { get; set; }
    public string? CouponCode { get; set; }
    public decimal CouponAmount { get; set; }
    public decimal PointsUsed { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
}