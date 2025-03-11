using System.ComponentModel.DataAnnotations;

namespace FreshVegCart.Shared.Dtos;

public class AddressDto
{
    public int Id { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string Name { get; set; }
    public bool IsDefault { get; set; }
}


