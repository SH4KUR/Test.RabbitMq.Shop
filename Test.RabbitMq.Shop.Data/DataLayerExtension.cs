using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Test.RabbitMq.Shop.Core.Interfaces;
using Test.RabbitMq.Shop.Data.Repositories;

namespace Test.RabbitMq.Shop.Data;

public static class DataLayerExtension
{
    public static IServiceCollection AddDataLayerDependencies(this IServiceCollection services)
    {
        services.AddDbContext<TestShopContext>(
            o => o.UseInMemoryDatabase("TestShopDb"));
        
        services.AddScoped<TestDataSeeder>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
            
        return services;
    }
}