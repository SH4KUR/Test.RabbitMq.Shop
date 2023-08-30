using MassTransit;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Common.Messages;

namespace Test.RabbitMq.Shop.Common.NotificationService.Consumers;

public class SendNotificationConsumer : IConsumer<ISendNotificationCommand>
{
    private readonly ILogger<SendNotificationConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    
    public SendNotificationConsumer(ILogger<SendNotificationConsumer> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<ISendNotificationCommand> context)
    {
        var message = context.Message;
        var jsonMessage = JsonConvert.SerializeObject(message);
        
        _logger.LogInformation($"ISendNotificationCommand message: {jsonMessage}");
        
        // imitation of sending an email
        _logger.LogWarning($"Notification on email: {DateTime.Now:G} - Order {message.OrderId}");
        if (IsSuccessUsingDnD())
        {
            await _publishEndpoint.Publish(new NotificationSentEvent(message.CorrelationId, message.OrderId));
        }
        else
        {
            throw new Exception("Failed because DnD");
        }
    }

    // gamification of successful notification sending
    private bool IsSuccessUsingDnD()
    {
        const int check = 13;
        var d20dice = new Random();
        var roll = d20dice.Next(1, 21);

        _logger.LogWarning($"DND: Notification success ({check}) roll - {roll}");
        
        return roll >= check;
    }
}