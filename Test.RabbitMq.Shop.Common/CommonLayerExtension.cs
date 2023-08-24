using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Test.RabbitMq.Shop.Common;

public static class CommonLayerExtension
{
    public static IServiceCollection AddCommonLayerDependencies(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("localhost", c =>
                {
                    c.Username("guest");
                    c.Password("guest");
                });
            });
        });
        
        return services;
    }
}