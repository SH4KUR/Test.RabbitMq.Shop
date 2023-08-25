using MassTransit;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Common.Messages;

namespace Test.RabbitMq.Shop.Common.NotificationService.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<OrderCreatedConsumer> _logger;
    
    public OrderCreatedConsumer(IPublishEndpoint publishEndpoint, ILogger<OrderCreatedConsumer> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;
        var jsonMessage = JsonConvert.SerializeObject(message);
        
        _logger.LogInformation($"OrderCreatedEvent message: {jsonMessage}");
        
        // imitation of sending an email
        _logger.LogWarning($"Notification on email: {DateTime.Now:G} - Order {message.OrderId}");
        
        _publishEndpoint.Publish(new NotificationSentEvent(message.CorrelationId, message.OrderId));
        
        return Task.CompletedTask;
    }
}