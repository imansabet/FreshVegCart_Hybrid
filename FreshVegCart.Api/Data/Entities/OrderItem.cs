using System.ComponentModel.DataAnnotations;

namespace FreshVegCart.Api.Data.Entities;

public class OrderItem 
{
    [Key]
    public long Id { get; set; }

    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    public int ProductId  { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; }
    public string ProductImageUrl { get; set; }
}
