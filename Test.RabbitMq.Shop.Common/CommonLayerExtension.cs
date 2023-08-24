using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Test.RabbitMq.Shop.Common.Messages;

namespace Test.RabbitMq.Shop.Common;

public static class CommonLayerExtension
{
    public static IServiceCollection AddCommonLayerDependencies(this IServiceCollection services)
    {
        services.AddScoped<OrderCreatedConsumer>();
        
        services.AddMassTransit(registrationConfigurator =>
        {
            registrationConfigurator.AddConsumer<OrderCreatedConsumer>();
            registrationConfigurator.AddConsumer<NotificationSentConsumer>();
            
            registrationConfigurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                factoryConfigurator.Host("localhost", hostConfigurator =>
                {
                    hostConfigurator.Username("guest");
                    hostConfigurator.Password("guest");
                });
                
                factoryConfigurator.ReceiveEndpoint("order_saga", consumer =>
                {
                    consumer.ConfigureConsumer<OrderCreatedConsumer>(context);
                    consumer.ConfigureConsumer<NotificationSentConsumer>(context);
                });
            });
        });
        
        return services;
    }
}