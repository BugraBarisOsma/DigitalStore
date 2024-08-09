namespace DigitalStore.Core.DTOs;

public class CouponRequestDTO
{
    public string Code { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
}