using System.Security.Claims;

namespace FreshVegCart.Shared;

public static class Extensions
{
    public static int GetUserId(this ClaimsPrincipal principal) =>
        Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}
