using MassTransit;

namespace Test.RabbitMq.Shop.Common.Messages;

public interface ISendNotificationEvent : CorrelatedBy<Guid>
{
    public Guid OrderId { get; set; }
}

public class SendNotificationEvent : ISendNotificationEvent
{
    public SendNotificationEvent(Guid correlationId, Guid orderId)
    {
        CorrelationId = correlationId;
        OrderId = orderId;
    }

    public Guid OrderId { get; set; }
    public Guid CorrelationId { get; }
}