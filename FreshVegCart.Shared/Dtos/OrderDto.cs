namespace FreshVegCart.Shared.Dtos;

public class OrderDto 
{
    public int Id { get; set; }

    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Remarks { get; set; }
    public string Status { get; set; } 
    public string Address { get; set; }
    public string AddressName { get; set; }
    public int TotalItems { get; set; }

}