using System.ComponentModel.DataAnnotations;

namespace FreshVegCart.Shared.Dtos;

public class RegisterDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    public string Mobile { get; set; }
    [Required]
    public string Password { get; set; }

}
