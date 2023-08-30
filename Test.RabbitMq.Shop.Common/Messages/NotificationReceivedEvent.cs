using MassTransit;

namespace Test.RabbitMq.Shop.Common.Messages;

public interface INotificationReceivedEvent : CorrelatedBy<Guid>
{
    public Guid OrderId { get; set; }
}

public class NotificationReceivedEvent : INotificationReceivedEvent
{
    public NotificationReceivedEvent(Guid correlationId, Guid orderId)
    {
        CorrelationId = correlationId;
        OrderId = orderId;
    }

    public Guid OrderId { get; set; }
    public Guid CorrelationId { get; }
}