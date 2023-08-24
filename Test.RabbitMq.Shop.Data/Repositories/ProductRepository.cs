using Test.RabbitMq.Shop.Core.Entities;
using Test.RabbitMq.Shop.Core.Interfaces;

namespace Test.RabbitMq.Shop.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly TestShopContext _context;

    public ProductRepository(TestShopContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Product> GetProducts()
    {
        return _context.Products;
    }

    public Product? GetProduct(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }
}