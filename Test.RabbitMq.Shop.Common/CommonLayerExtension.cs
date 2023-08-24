using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Test.RabbitMq.Shop.Common;

public static class CommonLayerExtension
{
    public static IServiceCollection AddCommonLayerDependencies(this IServiceCollection services)
    {
        services.AddScoped<OrderCreatedConsumer>();
        
        services.AddMassTransit(x =>
        {
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
        
        return services;
    }
}