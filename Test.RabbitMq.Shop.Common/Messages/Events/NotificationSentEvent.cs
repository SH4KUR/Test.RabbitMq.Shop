using MassTransit;

namespace Test.RabbitMq.Shop.Common.Messages.Events;

public interface INotificationSentEvent : CorrelatedBy<Guid>
{
    public Guid OrderId { get; set; }
}

public class NotificationSentEvent : INotificationSentEvent
{
    public NotificationSentEvent(Guid correlationId, Guid orderId)
    {
        CorrelationId = correlationId;
        OrderId = orderId;
    }

    public Guid OrderId { get; set; }
    public Guid CorrelationId { get; }
}