namespace DigitalStore.Core.DTOs;

public class OrderDetailResponseDTO
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }

    public ProductResponseDTO Product { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}