using DigitalStore.Core.Domains;
using DigitalStore.Core.Domains.Base;

public class Category : BaseEntity
{

    public string Name { get; set; }
    public string Url { get; set; }
    public string Tag { get; set; }
    public List<ProductCategory> ProductCategories { get; set; } 
}