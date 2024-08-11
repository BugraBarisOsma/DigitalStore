namespace DigitalStore.Core.Domains.Base;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedDate { get; set; }
    public bool IsActive { get; set; } = true;
}