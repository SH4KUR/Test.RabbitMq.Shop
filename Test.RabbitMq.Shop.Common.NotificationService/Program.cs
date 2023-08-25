using MassTransit;
using Test.RabbitMq.Shop.Common.NotificationService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    
    x.AddConsumer<OrderCreatedConsumer>();
    x.AddConsumer<NotificationSentConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
                
        cfg.ReceiveEndpoint("order_saga", c =>
        {
            c.ConfigureConsumer<OrderCreatedConsumer>(context);
            c.ConfigureConsumer<NotificationSentConsumer>(context);
        });
    });
});

var app = builder.Build();

app.Run();