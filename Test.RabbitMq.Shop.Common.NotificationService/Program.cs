using MassTransit;
using Test.RabbitMq.Shop.Common.NotificationService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    //x.SetKebabCaseEndpointNameFormatter();

    // x.AddSagaStateMachine<OrderStateMachine, OrderState>()
    //     .Endpoint(e => { e.Name = "order-saga"; })
    //     .InMemoryRepository();
    
    x.AddConsumer<SendNotificationConsumer>();
    // x.AddRequestClient<SendNotificationConsumer>();
    x.AddConsumer<CheckNotificationConsumer>();
    // x.AddRequestClient<CheckNotificationConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
                
        cfg.ReceiveEndpoint("order-saga", c =>
        {
            // c.ConfigureSaga<OrderState>(context);
            
            c.UseMessageRetry(r => r.Interval(5, 1000));
            
            c.ConfigureConsumer<SendNotificationConsumer>(context);
            c.ConfigureConsumer<CheckNotificationConsumer>(context);
        });
        
        //cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.Run();