namespace DigitalStore.Core.DTOs;

public class CheckoutResultDTO
{
    public decimal TotalAmount { get; set; }
    public decimal PointsEarned { get; set; }
    public string UserId { get; set; }
}