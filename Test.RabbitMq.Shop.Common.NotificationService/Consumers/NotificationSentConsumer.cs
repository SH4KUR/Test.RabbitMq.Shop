using MassTransit;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Common.Messages;

namespace Test.RabbitMq.Shop.Common.NotificationService.Consumers;

public class NotificationSentConsumer : IConsumer<NotificationSentEvent>
{
    private readonly ILogger<NotificationSentConsumer> _logger;

    public NotificationSentConsumer(ILogger<NotificationSentConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<NotificationSentEvent> context)
    {
        var message = context.Message;
        var jsonMessage = JsonConvert.SerializeObject(message);
        
        _logger.LogInformation($"NotificationSentConsumer message: {jsonMessage}");
        
        // simulate receiving an email
        _logger.LogWarning("Notification message was received");
        
        return Task.CompletedTask;
    }
}