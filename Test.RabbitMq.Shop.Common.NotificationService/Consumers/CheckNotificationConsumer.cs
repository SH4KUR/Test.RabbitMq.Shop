using MassTransit;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Common.Messages;

namespace Test.RabbitMq.Shop.Common.NotificationService.Consumers;

public class CheckNotificationConsumer : IConsumer<INotificationSentEvent>
{
    private readonly ILogger<CheckNotificationConsumer> _logger;

    public CheckNotificationConsumer(ILogger<CheckNotificationConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<INotificationSentEvent> context)
    {
        var message = context.Message;
        var jsonMessage = JsonConvert.SerializeObject(message);
        
        _logger.LogInformation($"INotificationSentEvent message: {jsonMessage}");
        
        // simulate receiving an email
        _logger.LogWarning("Notification message was received");
        
        return Task.CompletedTask;
    }
}