using DigitalStore.Core.Domains;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Tag { get; set; }
    public List<ProductCategory> ProductCategories { get; set; } 
}