using MassTransit;

namespace Test.RabbitMq.Shop.Common.StateMachineService.Saga;

public class OrderState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public Guid OrderId { get; set; }
    public string CurrentState { get; set; }
}