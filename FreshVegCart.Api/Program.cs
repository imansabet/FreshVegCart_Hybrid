using FreshVegCart.Api.Data;
using FreshVegCart.Api.Data.Entities;
using FreshVegCart.Api.Endpoints;
using FreshVegCart.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(options =>
{
    var ConnectionStrings = builder.Configuration.GetConnectionString("Default");
    options.UseSqlServer(ConnectionStrings);
});


builder.Services.AddTransient<AuthService>()
    .AddTransient<OrderService>()
    .AddTransient<ProductService>()
    .AddTransient<UserService>()
    .AddTransient<IPasswordHasher<User>,PasswordHasher<User>>()
    ;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var secretKey = builder.Configuration.GetValue<string>("Jwt:Secret");
        var symmetricKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            IssuerSigningKey = symmetricKey,
            ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
            //ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
            ValidateIssuer = true,
            //ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    ApplyDbMigrations(app.Services);

}

app.UseHttpsRedirection();

app.UseAuthentication()
    .UseAuthorization();


app.MapProductEndpoints()
    .MapAuthEndpoints()
    .MapUserEndpoints()
    .MapOrderEndpoints();



app.Run();



static void ApplyDbMigrations(IServiceProvider sp)
{
    var scope = sp.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }


}