namespace DigitalStore.Core.DTOs;

public class ProductRequestDTO
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public int Stock { get; set; }
    public double RewardPointsPercentage { get; set; }
    public decimal MaxRewardPoints { get; set; }
}