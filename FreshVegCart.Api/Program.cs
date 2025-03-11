using FreshVegCart.Api.Data;
using FreshVegCart.Api.Data.Entities;
using FreshVegCart.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();
