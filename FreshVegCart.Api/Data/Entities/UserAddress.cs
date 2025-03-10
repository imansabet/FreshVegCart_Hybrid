using System.ComponentModel.DataAnnotations;

namespace FreshVegCart.Api.Data.Entities;

public class UserAddress 
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    [Required, MaxLength(250)]

    public string Address { get; set; }

    [Required,MaxLength(20)]
    public string Name { get; set; }

    public bool IsDefault { get; set; }
}
