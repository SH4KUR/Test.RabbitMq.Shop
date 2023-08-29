using Test.RabbitMq.Shop.Core.Entities;

namespace Test.RabbitMq.Shop.Data;

public class TestDataSeeder
{
    private readonly TestShopContext _context;

    public TestDataSeeder(TestShopContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        SeedProducts();
        SeedOrders();
        
        _context.SaveChanges();
    }

    private void SeedOrders()
    {
        _context.Orders.Add(new Order
        {
            Id = Guid.NewGuid(),
            ProductId = 1,
            ProductQuantity = 1,
            OrderPrice = (decimal)99.99,
            CreationDateTime = DateTime.Now
        });
    }
    
    private void SeedProducts()
    {
        _context.Products.AddRange(
            new Product
            {
                Id = 1,
                Name = "First Test Product",
                Price = (decimal)99.99,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ut tincidunt."
            },
            new Product
            {
                Id = 2,
                Name = "Second Test Product",
                Price = (decimal)235.00,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas et nulla."
            },
            new Product
            {
                Id = 3,
                Name = "Third Test Product",
                Price = (decimal)128.56,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam ut efficitur."
            });
    }
}