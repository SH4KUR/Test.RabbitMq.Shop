using MassTransit;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Common.Messages;
using Test.RabbitMq.Shop.Core.Entities;
using Test.RabbitMq.Shop.Core.Interfaces;

namespace Test.RabbitMq.Shop.Common;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly INotificationRepository _notificationRepository;

    public OrderCreatedConsumer(IPublishEndpoint publishEndpoint, INotificationRepository notificationRepository)
    {
        _publishEndpoint = publishEndpoint;
        _notificationRepository = notificationRepository;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;
        var jsonMessage = JsonConvert.SerializeObject(message);
        
        Console.WriteLine($"OrderCreatedEvent message: {jsonMessage}");
        
        // imitation of sending an email
        _notificationRepository.AddNotification(new Notification()
        {
            Id = Guid.NewGuid(),
            OrderId = message.OrderId,
            NotificationDateTime = DateTime.Now
        });
        
        await _publishEndpoint.Publish(new NotificationSentEvent(message.CorrelationId, message.OrderId));
    }
}