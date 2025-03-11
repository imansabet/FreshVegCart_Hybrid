using System.ComponentModel.DataAnnotations;

namespace FreshVegCart.Shared.Dtos;

public class OrderItemSaveDto 
{
    [Required]
    public int ProductId  { get; set; }

    [Required , Range(1,int.MaxValue)]
    public int Quantity { get; set; }

}