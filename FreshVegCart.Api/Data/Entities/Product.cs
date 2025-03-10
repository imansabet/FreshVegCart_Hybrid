using System.ComponentModel.DataAnnotations;

namespace FreshVegCart.Api.Data.Entities;

public class Product 
{
    [Key]
    public int Id { get; set; }
    [Required,MaxLength(50)]
    public string Name { get; set; }
    [Required, MaxLength(200)]
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    [Required,MaxLength (10)]
    public string Unit { get; set; }

    public static Product[] GetSeedData()
    {
        const string ImageUrlPrefix = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/";

        Product[] products = [
            new () { Id = 1, Name = "Avocado", ImageUrl = $"{ImageUrlPrefix}avocado.png", Unit = "each", Price = 1.99m },
            new () { Id = 2, Name = "Beet", ImageUrl = $"{ImageUrlPrefix}beet.png", Unit = "each", Price = 0.99m },
            new () { Id = 3, Name = "Bell Pepper", ImageUrl = $"{ImageUrlPrefix}bell_pepper.png", Unit = "each", Price = 1.50m },
            new () { Id = 4, Name = "Broccoli", ImageUrl = $"{ImageUrlPrefix}broccoli.png", Unit = "each", Price = 2.00m },
            new () { Id = 5, Name = "Cabbage", ImageUrl = $"{ImageUrlPrefix}cabbage.png", Unit = "each", Price = 1.20m },
            new () { Id = 6, Name = "Capsicum", ImageUrl = $"{ImageUrlPrefix}capsicum.png", Unit = "each", Price = 1.80m },
            new () { Id = 7, Name = "Carrot", ImageUrl = $"{ImageUrlPrefix}carrot.png", Unit = "kg", Price = 0.80m },
            new () { Id = 8, Name = "Cauliflower", ImageUrl = $"{ImageUrlPrefix}cauliflower.png", Unit = "each", Price = 2.50m },
            new () { Id = 9, Name = "Coriander", ImageUrl = $"{ImageUrlPrefix}coriander.png", Unit = "bunch", Price = 0.70m },
            new () { Id = 10, Name = "Corn", ImageUrl = $"{ImageUrlPrefix}corn.png", Unit = "each", Price = 1.00m },
            new () { Id = 11, Name = "Cucumber", ImageUrl = $"{ImageUrlPrefix}cucumber.png", Unit = "each", Price = 0.90m },
            new () { Id = 12, Name = "Eggplant", ImageUrl = $"{ImageUrlPrefix}eggplant.png", Unit = "each", Price = 1.75m },
            new () { Id = 13, Name = "Farmer", ImageUrl = $"{ImageUrlPrefix}farmer.png", Unit = "each", Price = 5.00m },
            new () { Id = 14, Name = "Ginger", ImageUrl = $"{ImageUrlPrefix}ginger.png", Unit = "kg", Price = 2.20m },
            new () { Id = 15, Name = "Green Beans", ImageUrl = $"{ImageUrlPrefix}green_beans.png", Unit = "kg", Price = 1.60m },
            new () { Id = 16, Name = "Ladyfinger", ImageUrl = $"{ImageUrlPrefix}ladyfinger.png", Unit = "kg", Price = 1.10m },
            new () { Id = 17, Name = "Lettuce", ImageUrl = $"{ImageUrlPrefix}lettuce.png", Unit = "each", Price = 1.30m },
            new () { Id = 18, Name = "Mushroom", ImageUrl = $"{ImageUrlPrefix}mushroom.png", Unit = "kg", Price = 2.80m },
            new () { Id = 19, Name = "Onion", ImageUrl = $"{ImageUrlPrefix}onion.png", Unit = "kg", Price = 0.60m },
            new () { Id = 20, Name = "Pea", ImageUrl = $"{ImageUrlPrefix}pea.png", Unit = "kg", Price = 1.40m },
            new () { Id = 21, Name = "Potato", ImageUrl = $"{ImageUrlPrefix}potato.png", Unit = "kg", Price = 0.50m },
            new () { Id = 22, Name = "Pumpkin", ImageUrl = $"{ImageUrlPrefix}pumpkin.png", Unit = "each", Price = 3.00m },
            new () { Id = 23, Name = "Radish", ImageUrl = $"{ImageUrlPrefix}radish.png", Unit = "bunch", Price = 0.85m },
            new () { Id = 24, Name = "Red Chili", ImageUrl = $"{ImageUrlPrefix}red_chili.png", Unit = "kg", Price = 1.50m },
            new () { Id = 25, Name = "Spinach", ImageUrl = $"{ImageUrlPrefix}spinach.png", Unit = "bunch", Price = 1.20m },
            new () { Id = 26, Name = "Tomato", ImageUrl = $"{ImageUrlPrefix}tomato.png", Unit = "kg", Price = 0.95m },
            new () { Id = 27, Name = "Turnip", ImageUrl = $"{ImageUrlPrefix}turnip.png", Unit = "each", Price = 0.75m },
            new () { Id = 28, Name = "Vegetables", ImageUrl = $"{ImageUrlPrefix}vegetables.png", Unit = "each", Price = 4.00m },
            new () { Id = 29, Name = "Yellow Capsicum", ImageUrl = $"{ImageUrlPrefix}yellow_capsicum.png", Unit = "each", Price = 1.80m }
            ];

        return products;
    }
}
