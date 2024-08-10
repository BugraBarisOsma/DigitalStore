using DigitalStore.Core.Domains.Base;

namespace DigitalStore.Core.Domains;

public class ProductCategory : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    
}