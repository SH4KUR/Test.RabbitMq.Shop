using MassTransit;

namespace Test.RabbitMq.Shop.Common.Messages.Events;

public interface IOrderCreatedEvent : CorrelatedBy<Guid>
{
    public Guid OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal OrderPrice { get; set; }
}

public class OrderCreatedEvent : IOrderCreatedEvent
{
    public OrderCreatedEvent(Guid orderId, int productId, decimal orderPrice)
    {
        CorrelationId = Guid.NewGuid();
        OrderId = orderId;
        ProductId = productId;
        OrderPrice = orderPrice;
    }

    public Guid OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal OrderPrice { get; set; }
    public Guid CorrelationId { get; }
}