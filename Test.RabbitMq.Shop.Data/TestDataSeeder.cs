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
            // Id = Guid.NewGuid(),
            // ProductId = new Guid("5FCAB8F3-E575-47B9-83D9-7BC62BBF6819"),
            Id = 1,
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
                // Id = new Guid("5FCAB8F3-E575-47B9-83D9-7BC62BBF6819"),
                Id = 1,
                Name = "First Test Product",
                Price = (decimal)99.99
            },
            new Product
            {
                // Id = new Guid("8500D76C-97A4-4751-B245-7C56DFBF3431"),
                Id = 2,
                Name = "Second Test Product",
                Price = (decimal)235.00
            },
            new Product
            {
                // Id = new Guid("F3466D83-A045-4A44-8A91-3AC8390110F0"),
                Id = 3,
                Name = "Third Test Product",
                Price = (decimal)128.56
            });
    }
}