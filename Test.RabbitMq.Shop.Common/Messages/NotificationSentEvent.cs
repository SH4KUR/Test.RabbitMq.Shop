namespace Test.RabbitMq.Shop.Common.Messages;

public class NotificationSentEvent : BaseEvent
{
    public NotificationSentEvent(Guid correlationId, int orderId) : base(correlationId)
    {
        OrderId = orderId;
    }

    public int OrderId { get; set; }
}