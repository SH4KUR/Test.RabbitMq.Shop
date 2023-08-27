using MassTransit;
using Test.RabbitMq.Shop.Common.StateMachineService.Saga;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5002/");

builder.Services.AddMassTransit(x =>
{
    //x.SetInMemorySagaRepositoryProvider();
    x.AddSagaStateMachine<OrderStateMachine, OrderState>()
        .Endpoint(e => { e.Name = "order-saga"; })
        .InMemoryRepository();
    //x.AddSagas(typeof(Program).Assembly);
    
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
          
        cfg.ReceiveEndpoint("order-saga", c =>
        {
            const int ConcurrencyLimit = 20; // this can go up, depending upon the database capacity

            c.PrefetchCount = ConcurrencyLimit;
            c.StateMachineSaga<OrderState>(context);
            
            // c.ConfigureSaga<OrderState>(context, s =>
            // {
            //     var partition = c.CreatePartitioner(ConcurrencyLimit);
            //     
            //     s.Message<OrderCreatedEvent>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
            //     s.Message<SendNotificationEvent>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
            //     s.Message<NotificationSentEvent>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
            // });
            
            c.UseMessageRetry(r => r.Interval(5, 1000));
        });
    });
});

var app = builder.Build();

app.Run();