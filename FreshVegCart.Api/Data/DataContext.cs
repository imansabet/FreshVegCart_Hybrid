using FreshVegCart.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreshVegCart.Api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem>  OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<UserAddress>  UserAddresses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasData(Product.GetSeedData());

    }
}
