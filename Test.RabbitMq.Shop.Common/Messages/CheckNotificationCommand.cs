using MassTransit;

namespace Test.RabbitMq.Shop.Common.Messages;

public interface ICheckNotificationCommand : CorrelatedBy<Guid>
{
    public Guid OrderId { get; set; }
}

public class CheckNotificationCommand : ICheckNotificationCommand
{
    public CheckNotificationCommand(Guid correlationId, Guid orderId)
    {
        CorrelationId = correlationId;
        OrderId = orderId;
    }

    public Guid OrderId { get; set; }
    public Guid CorrelationId { get; }
}