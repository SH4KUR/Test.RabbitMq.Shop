using MassTransit;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Common.Messages;

namespace Test.RabbitMq.Shop.Common.NotificationService.Consumers;

public class CheckNotificationConsumer : IConsumer<ICheckNotificationEvent>
{
    private readonly ILogger<CheckNotificationConsumer> _logger;

    public CheckNotificationConsumer(ILogger<CheckNotificationConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ICheckNotificationEvent> context)
    {
        var message = context.Message;
        var jsonMessage = JsonConvert.SerializeObject(message);
        
        _logger.LogInformation($"ICheckNotificationEvent message: {jsonMessage}");
        
        // simulated email verification
        _logger.LogWarning($"Notification message for {message.OrderId} order was received");
        
        return Task.CompletedTask;
    }
}