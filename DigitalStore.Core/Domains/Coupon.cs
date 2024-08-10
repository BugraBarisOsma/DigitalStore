using DigitalStore.Core.Domains.Base;

public class Coupon : BaseEntity
{

    public string Code { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpiryDate { get; set; }

}