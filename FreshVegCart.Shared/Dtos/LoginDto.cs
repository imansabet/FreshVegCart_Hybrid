using FreshVegCart.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace FreshVegCart.Shared.Dtos;

public class LoginDto
{
    [Required] 
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }

}
