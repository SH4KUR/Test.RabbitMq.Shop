using Test.RabbitMq.Shop.Core.Entities;

namespace Test.RabbitMq.Shop.Core.Interfaces;

public interface IProductRepository
{
    public IEnumerable<Product> GetProducts();
    public Product? GetProduct(Guid id);
}