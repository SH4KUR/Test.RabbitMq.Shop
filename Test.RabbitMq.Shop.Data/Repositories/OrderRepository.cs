using Test.RabbitMq.Shop.Core.Entities;
using Test.RabbitMq.Shop.Core.Interfaces;

namespace Test.RabbitMq.Shop.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly TestShopContext _context;

    public OrderRepository(TestShopContext context)
    {
        _context = context;
    }
    
    public void AddOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }
    
    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public IEnumerable<Order> GetOrders()
    {
        return _context.Orders;
    }
}