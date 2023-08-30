using MassTransit;

namespace Test.RabbitMq.Shop.Common.Messages.Commands;

public interface ISendNotificationCommand : CorrelatedBy<Guid>
{
    public Guid OrderId { get; set; }
}

public class SendNotificationCommand : ISendNotificationCommand
{
    public SendNotificationCommand(Guid correlationId, Guid orderId)
    {
        CorrelationId = correlationId;
        OrderId = orderId;
    }

    public Guid OrderId { get; set; }
    public Guid CorrelationId { get; }
}