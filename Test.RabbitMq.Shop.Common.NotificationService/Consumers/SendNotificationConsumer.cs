using MassTransit;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Common.Messages;

namespace Test.RabbitMq.Shop.Common.NotificationService.Consumers;

public class SendNotificationConsumer : IConsumer<ISendNotificationEvent>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly ILogger<SendNotificationConsumer> _logger;
    
    public SendNotificationConsumer(ILogger<SendNotificationConsumer> logger, ISendEndpointProvider sendEndpointProvider)
    {
        _logger = logger;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task Consume(ConsumeContext<ISendNotificationEvent> context)
    {
        var message = context.Message;
        var jsonMessage = JsonConvert.SerializeObject(message);
        
        _logger.LogInformation($"ISendNotificationEvent message: {jsonMessage}");
        
        // imitation of sending an email
        _logger.LogWarning($"Notification on email: {DateTime.Now:G} - Order {message.OrderId}");
        
        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(
            new Uri($"queue:{QueueNames.OrderSagaQueueName}"));
    
        if (IsSuccessUsingDnD())
        {
            await sendEndpoint.Send(new NotificationSentEvent(message.CorrelationId, message.OrderId));
        }
        else
        {
            throw new Exception("Failed because DnD");
        }
    }

    // gamification of successful notification sending
    private bool IsSuccessUsingDnD()
    {
        const int check = 11;
        var d20dice = new Random();
        var roll = d20dice.Next(1, 20);

        _logger.LogWarning($"Notification success ({check}): roll - {roll}");
        
        return roll >= check;
    }
}