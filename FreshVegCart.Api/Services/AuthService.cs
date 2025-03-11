using Azure.Identity;
using FreshVegCart.Api.Data;
using FreshVegCart.Api.Data.Entities;
using FreshVegCart.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreshVegCart.Api.Services;

public class AuthService
{
    private readonly DataContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(DataContext context , IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;

    }
    public async Task<ApiResult> RegisterAsync(RegisterDto dto) 
    {
        if(await _context.Users.AnyAsync(u => u.Email == dto.Email)) 
        {
            return ApiResult.Fail("Email Already Exists.");
        }
        var user = new User
        {
            Email = dto.Email,
            Mobile = dto.Mobile,
            Name = dto.Name
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return ApiResult.Success();
        }
        catch (Exception ex) 
        {

            return ApiResult.Fail(ex.Message);
        } 

    }


    public async Task<ApiResult<LoggedInUser>> LoginAsync(LoginDto dto) 
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.UserName);
        if(user is null)
        {
            return ApiResult<LoggedInUser>.Fail("User Does Not Exist .");
        }
        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if(verificationResult != PasswordVerificationResult.Success) 
        {
            return ApiResult<LoggedInUser>.Fail("Incorrect Password");
        }

        //Generate Token
        var jwt = "JWT _TOKEN";

        var loggedInUser = new LoggedInUser(user.Id, user.Name, user.Email, jwt);
        return ApiResult<LoggedInUser>.Success(loggedInUser);

    }


}
