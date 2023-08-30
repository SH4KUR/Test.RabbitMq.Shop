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
                Description = "Maecenas euismod justo ipsum, eu tincidunt arcu ornare a. Fusce molestie quam in massa placerat."
            },
            new Product
            {
                Id = 2,
                Name = "Second Test Product",
                Price = (decimal)235.00,
                Description = "Quisque quis augue eu urna ullamcorper tincidunt. In facilisis dignissim felis, non rutrum diam maximus."
            },
            new Product
            {
                Id = 3,
                Name = "Third Test Product",
                Price = (decimal)128.56,
                Description = "Pellentesque vitae ipsum non sapien volutpat lobortis nec eget lorem. Praesent et arcu et orci."
            });
    }
}