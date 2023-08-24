namespace Test.RabbitMq.Shop.Common.Messages;

public abstract class BaseEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime EventCreationDate { get; set; } = DateTime.Now;
}