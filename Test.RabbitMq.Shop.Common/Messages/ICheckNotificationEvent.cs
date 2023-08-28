using MassTransit;

namespace Test.RabbitMq.Shop.Common.Messages;

public interface ICheckNotificationEvent : CorrelatedBy<Guid>
{
    public Guid OrderId { get; set; }
}

public class CheckNotificationEvent : ICheckNotificationEvent
{
    public CheckNotificationEvent(Guid correlationId, Guid orderId)
    {
        CorrelationId = correlationId;
        OrderId = orderId;
    }

    public Guid OrderId { get; set; }
    public Guid CorrelationId { get; }
}