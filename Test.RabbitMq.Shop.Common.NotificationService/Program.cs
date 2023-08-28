using MassTransit;
using Test.RabbitMq.Shop.Common;
using Test.RabbitMq.Shop.Common.NotificationService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.AddConsumer<SendNotificationConsumer>();
    x.AddConsumer<CheckNotificationConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        
        cfg.ReceiveEndpoint(QueueNames.NotificationQueueName, c =>
        {
            c.UseMessageRetry(r => r.Interval(3, 1000));
            
            c.ConfigureConsumer<SendNotificationConsumer>(context);
            c.ConfigureConsumer<CheckNotificationConsumer>(context);
        });
    });
});

var app = builder.Build();

app.Run();