namespace DigitalStore.Core.DTOs;

public class OrderItemDTO
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}