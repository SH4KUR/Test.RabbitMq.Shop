using MassTransit;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Common.Messages.Commands;
using Test.RabbitMq.Shop.Common.Messages.Events;

namespace Test.RabbitMq.Shop.Common.NotificationService.Consumers;

public class CheckNotificationConsumer : IConsumer<ICheckNotificationCommand>
{
    private readonly ILogger<CheckNotificationConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public CheckNotificationConsumer(ILogger<CheckNotificationConsumer> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<ICheckNotificationCommand> context)
    {
        var message = context.Message;
        var jsonMessage = JsonConvert.SerializeObject(message);
        
        _logger.LogInformation($"ICheckNotificationCommand message: {jsonMessage}");
        
        // simulated email verification
        await _publishEndpoint.Publish(new NotificationReceivedEvent(message.CorrelationId, message.OrderId));
        
        _logger.LogWarning($"Notification message for {message.OrderId} order was received");
    }
}