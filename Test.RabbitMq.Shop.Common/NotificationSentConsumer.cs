using MassTransit;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Common.Messages;
using Test.RabbitMq.Shop.Core.Interfaces;

namespace Test.RabbitMq.Shop.Common;

public class NotificationSentConsumer : IConsumer<NotificationSentEvent>
{
    public async Task Consume(ConsumeContext<NotificationSentEvent> context)
    {
        var message = context.Message;
        var jsonMessage = JsonConvert.SerializeObject(message);
        
        Console.WriteLine($"NotificationSentConsumer message: {jsonMessage}");
    }
}