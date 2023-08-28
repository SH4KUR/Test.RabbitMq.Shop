using MassTransit;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Common.Messages;

namespace Test.RabbitMq.Shop.Common.StateMachineService;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    private readonly ILogger<OrderStateMachine> _logger;
    
    public Event<IOrderCreatedEvent> OrderCreatedEvent { get; set; }
    public Event<INotificationSentEvent> NotificationSentEvent { get; set; }
    public Event<Fault<ISendNotificationEvent>> SendNotificationFaultEvent { get; set; }
    
    public State OrderCreated { get; set; }
    public State SendNotificationFault { get; set; }
    public State NotificationSent { get; set; }
    
    public OrderStateMachine(ILogger<OrderStateMachine> logger)
    {
        _logger = logger;
        InstanceState(x => x.CurrentState);

        Event(() => OrderCreatedEvent);
        Event(() => NotificationSentEvent);
        Event(() => SendNotificationFaultEvent,
            x => x.CorrelateById(
                ctx => ctx.InitiatorId ?? ctx.Message.Message.CorrelationId));
        
        Initially(
            When(OrderCreatedEvent)
                .Then(ctx => 
                    _logger.LogWarning($"OrderCreatedEvent message: {JsonConvert.SerializeObject(ctx.Message)}"))
                .Then(ctx => ctx.Saga.OrderId = ctx.Message.OrderId)
                .Send(new Uri($"queue:{QueueNames.NotificationQueueName}"),
                    ctx => 
                        new SendNotificationEvent(ctx.Saga.CorrelationId, ctx.Message.OrderId))
                .TransitionTo(OrderCreated));

        During(OrderCreated,
            When(SendNotificationFaultEvent)
                .Then(ctx =>
                    _logger.LogError(
                        $"SendNotificationFaultEvent message: {JsonConvert.SerializeObject(ctx.Message)}"))
                .TransitionTo(SendNotificationFault));
        
        During(OrderCreated,
            When(NotificationSentEvent)
                .Then(ctx => 
                    _logger.LogWarning($"NotificationSentEvent message: {JsonConvert.SerializeObject(ctx.Message)}"))
                .Send(new Uri($"queue:{QueueNames.NotificationQueueName}"),
                    ctx => 
                        new CheckNotificationEvent(ctx.Saga.CorrelationId, ctx.Message.OrderId))
                .TransitionTo(NotificationSent)
                .Finalize());
    }
}