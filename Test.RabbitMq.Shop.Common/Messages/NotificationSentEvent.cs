namespace Test.RabbitMq.Shop.Common.Messages;

public class NotificationSentEvent : BaseEvent
{
    public NotificationSentEvent(Guid correlationId, Guid orderId) : base(correlationId)
    {
        OrderId = orderId;
    }

    public Guid OrderId { get; set; }
}