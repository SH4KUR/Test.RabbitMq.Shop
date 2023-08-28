namespace Test.RabbitMq.Shop.Common;

public static class QueueNames
{
    public static string OrderSagaQueueName => "order_saga";
    public static string NotificationQueueName => "order_notification";
}