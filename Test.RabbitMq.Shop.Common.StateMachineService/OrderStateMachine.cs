using MassTransit;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Common.Messages;

namespace Test.RabbitMq.Shop.Common.StateMachineService;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    private readonly ILogger<OrderStateMachine> _logger;
    
    public Event<IOrderCreatedEvent> OrderCreatedEvent { get; set; }
    public Event<INotificationSentEvent> NotificationSentEvent { get; set; }
    public Event<INotificationReceivedEvent> NotificationReceivedEvent { get; set; }
    public Event<Fault<ISendNotificationCommand>> SendNotificationFaultCommand { get; set; }
    
    public State OrderCreated { get; set; }
    public State SendNotificationFault { get; set; }
    public State NotificationSent { get; set; }
    public State NotificationReceived { get; set; }
    
    public OrderStateMachine(ILogger<OrderStateMachine> logger)
    {
        _logger = logger;
        InstanceState(x => x.CurrentState);

        Event(() => OrderCreatedEvent);
        Event(() => NotificationSentEvent);
        Event(() => NotificationReceivedEvent);
        Event(() => SendNotificationFaultCommand,
            x => x.CorrelateById(
                ctx => ctx.InitiatorId ?? ctx.Message.Message.CorrelationId));
        
        Initially(
            When(OrderCreatedEvent)
                .Then(ctx => 
                    _logger.LogWarning($"OrderCreatedEvent message: {JsonConvert.SerializeObject(ctx.Message)}"))
                .Then(ctx => ctx.Saga.OrderId = ctx.Message.OrderId)
                .Send(new Uri($"queue:{QueueNames.NotificationQueueName}"),
                    ctx => 
                        new SendNotificationCommand(ctx.Saga.CorrelationId, ctx.Message.OrderId))
                .TransitionTo(OrderCreated));

        During(OrderCreated,
            When(SendNotificationFaultCommand)
                .Then(ctx =>
                    _logger.LogError(
                        $"SendNotificationFaultCommand message: {JsonConvert.SerializeObject(ctx.Message)}"))
                .TransitionTo(SendNotificationFault));

        During(OrderCreated,
            When(NotificationSentEvent)
                .Then(ctx =>
                    _logger.LogWarning($"NotificationSentEvent message: {JsonConvert.SerializeObject(ctx.Message)}"))
                .Send(new Uri($"queue:{QueueNames.NotificationQueueName}"),
                    ctx =>
                        new CheckNotificationCommand(ctx.Saga.CorrelationId, ctx.Message.OrderId))
                .TransitionTo(NotificationSent));
        
        During(NotificationSent,
            When(NotificationReceivedEvent)
                .Then(ctx => 
                    _logger.LogWarning($"NotificationReceivedEvent message: {JsonConvert.SerializeObject(ctx.Message)}"))
                .TransitionTo(NotificationReceived)
                .Finalize());
    }
}