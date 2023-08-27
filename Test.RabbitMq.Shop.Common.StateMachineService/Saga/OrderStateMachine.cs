using MassTransit;
using Test.RabbitMq.Shop.Common.Messages;

namespace Test.RabbitMq.Shop.Common.StateMachineService.Saga;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    private readonly ILogger<OrderStateMachine> _logger;
    
    private Event<IOrderCreatedEvent> OrderCreatedEvent { get; set; }
    private Event<ISendNotificationEvent> SendNotificationEvent { get; set; }
    private Event<INotificationSentEvent> NotificationSentEvent { get; set; }

    private State OrderCreated { get; set; } = null!;
    private State NotificationSent { get; set; } = null!;
    
    public OrderStateMachine(ILogger<OrderStateMachine> logger)
    {
        _logger = logger;
        
        InstanceState(x => x.CurrentState);

        Event(() => OrderCreatedEvent);
        Event(() => SendNotificationEvent);
        Event(() => NotificationSentEvent);
        
        // Event(() => OrderCreatedEvent,
        //     x => x.CorrelateById(
        //         ctx => ctx.Message.CorrelationId));
        // Event(() => SendNotificationEvent,
        //     x => x.CorrelateById(
        //         ctx => ctx.Message.CorrelationId));
        // Event(() => NotificationSentEvent,
        //     x => x.CorrelateById(
        //         ctx => ctx.Message.CorrelationId));
        
        Initially(
            When(OrderCreatedEvent)
                .Then(context => _logger.LogWarning($"OrderStateMachine received OrderCreatedEvent: {context.Message}"))
                .Then(context => context.Saga.OrderId = context.Message.OrderId)
                // .Publish(ctx 
                //     => new SendNotificationEvent(ctx.Saga.CorrelationId, ctx.Message.OrderId))
                .Send(new Uri($"queue:order-saga"),
                    ctx => new SendNotificationEvent(ctx.Saga.CorrelationId, ctx.Message.OrderId))
                .TransitionTo(OrderCreated));
        
        During(OrderCreated,
            When(NotificationSentEvent)
                .Then(context => _logger.LogWarning($"OrderStateMachine received NotificationSentEvent: {context.Message}"))
                .TransitionTo(NotificationSent)
                .Finalize());
    }
}