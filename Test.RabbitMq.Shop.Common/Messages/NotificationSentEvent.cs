namespace Test.RabbitMq.Shop.Common.Messages;

public class NotificationSentEvent
{
    public NotificationSentEvent(Guid orderId)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        EventDateTime = DateTime.Now;
    }

    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public DateTime EventDateTime { get; set; }
}