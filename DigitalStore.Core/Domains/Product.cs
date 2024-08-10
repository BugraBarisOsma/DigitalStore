using DigitalStore.Core.Domains;
using DigitalStore.Core.Domains.Base;

public class Product :BaseEntity
{

    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public List<ProductCategory> ProductCategories { get; set; } 
    public double RewardPointsPercentage { get; set; }
    public decimal MaxRewardPoints { get; set; }
}