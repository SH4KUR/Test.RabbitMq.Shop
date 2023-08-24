namespace Test.RabbitMq.Shop.Common.Messages;

public class NotificationSentEvent : BaseEvent
{
    public NotificationSentEvent(Guid orderId)
    {
        OrderId = orderId;
    }

    public Guid OrderId { get; set; }
}