using MassTransit;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Common.Messages;

namespace Test.RabbitMq.Shop.Common.NotificationService.Consumers;

public class SendNotificationConsumer : IConsumer<ISendNotificationEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly ILogger<SendNotificationConsumer> _logger;
    
    public SendNotificationConsumer(IPublishEndpoint publishEndpoint, ILogger<SendNotificationConsumer> logger, ISendEndpointProvider sendEndpointProvider)
    {
        _publishEndpoint = publishEndpoint;
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
        
        // var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:order-saga"));
        // await sendEndpoint.Send(new NotificationSentEvent(message.CorrelationId, message.OrderId));
        
        await _publishEndpoint.Publish(new NotificationSentEvent(message.CorrelationId, message.OrderId));
    }
}