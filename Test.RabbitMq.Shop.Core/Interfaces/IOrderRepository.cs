using Test.RabbitMq.Shop.Core.Entities;

namespace Test.RabbitMq.Shop.Core.Interfaces;

public interface IOrderRepository
{
    public void AddOrder(Order order);
    public IEnumerable<Order> GetOrders();
}