namespace Test.RabbitMq.Shop.Common.Messages;

public abstract class BaseEvent
{
    protected BaseEvent(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime EventCreationDate { get; set; } = DateTime.Now;
    public Guid CorrelationId { get; set; }
}