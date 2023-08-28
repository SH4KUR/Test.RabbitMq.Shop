namespace Test.RabbitMq.Shop.Common;

public static class QueueNames
{
    public static string OrderSagaQueueName => "order-saga";
    public static string NotificationQueueName => "order-notification";
}