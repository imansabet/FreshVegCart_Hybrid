using Azure.Identity;
using FreshVegCart.Api.Data;
using FreshVegCart.Api.Data.Entities;
using FreshVegCart.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FreshVegCart.Api.Services;

public class AuthService
{
    private readonly DataContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthService(DataContext context , IPasswordHasher<User> passwordHasher , IConfiguration configuration)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
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

        //Generate Jwt Token
        var jwt = GenerateToken(user);

        var loggedInUser = new LoggedInUser(user.Id, user.Name, user.Email, jwt);
        return ApiResult<LoggedInUser>.Success(loggedInUser);

    }

    private string GenerateToken(User user) 
    {
        Claim[] claims =
                    
            [
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.Name),
                new Claim(ClaimTypes.Email , user.Email)
            ];

        var secretKey = _configuration.GetValue<string>("Jwt:Secret");
        var symmetricKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

        var signinCred = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("Jwt:Issuer"),
            //audience: _configuration.GetValue<string>("Jwt:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpireInMinutes")),
            signingCredentials: signinCred
            );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return token;
    }

}
 