using MassTransit;
using Test.RabbitMq.Shop.Common;
using Test.RabbitMq.Shop.Common.StateMachineService;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5002/");

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    
    x.AddSagaStateMachine<OrderStateMachine, OrderState>()
        .Endpoint(e => { e.Name = QueueNames.OrderSagaQueueName; })
        .InMemoryRepository();
    
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
          
        cfg.ReceiveEndpoint(QueueNames.OrderSagaQueueName, c =>
        {
            c.PrefetchCount = 20;
            c.UseMessageRetry(r => r.Interval(5, 1000));

            c.StateMachineSaga<OrderState>(context);
        });
    });
});

var app = builder.Build();

app.Run();