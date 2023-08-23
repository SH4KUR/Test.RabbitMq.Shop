namespace Test.RabbitMq.Shop.Common.Messages;

public class NotificationSentEvent
{
    public NotificationSentEvent(Guid id, Guid orderId)
    {
        Id = id;
        OrderId = orderId;
        EventDateTime = DateTime.Now;
    }

    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public DateTime EventDateTime { get; set; }
}