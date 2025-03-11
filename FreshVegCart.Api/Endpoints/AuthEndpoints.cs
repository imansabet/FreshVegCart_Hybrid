using FreshVegCart.Api.Services;
using FreshVegCart.Shared.Dtos;

namespace FreshVegCart.Api.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {

        var authGroup = app.MapGroup("/api/auth").WithTags("Auth");

        app.MapPost("/register", async (RegisterDto dto, AuthService service) =>
        {
            return Results.Ok(await service.RegisterAsync(dto));
        })
        .Produces<ApiResult>()
        .WithName("Register");

        app.MapPost("/login", async (LoginDto dto, AuthService service) =>
        {
            return Results.Ok(await service.LoginAsync(dto));
        })
       .Produces<ApiResult<LoggedInUser>>()
       .WithName("Register");

        return app;
    }
}

