namespace Test.RabbitMq.Shop.Common.Messages;

public class OrderCreatedEvent : BaseEvent
{
    public OrderCreatedEvent(int productId, int productQuantity, decimal orderPrice)
    {
        ProductId = productId;
        ProductQuantity = productQuantity;
        OrderPrice = orderPrice;
    }

    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public decimal OrderPrice { get; set; }
}