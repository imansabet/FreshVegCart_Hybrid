namespace FreshVegCart.Shared.Dtos;

public class OrderItemDto
{
    public long Id { get; set; }

    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; }
    public string ProductImageUrl { get; set; }
}