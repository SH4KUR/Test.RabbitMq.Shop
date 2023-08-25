namespace Test.RabbitMq.Shop.Common.Messages;

public class OrderCreatedEvent : BaseEvent
{
    public OrderCreatedEvent(Guid correlationId, Guid orderId, int productId, int productQuantity, decimal orderPrice) 
        : base(correlationId)
    {
        OrderId = orderId;
        ProductId = productId;
        ProductQuantity = productQuantity;
        OrderPrice = orderPrice;
    }

    public Guid OrderId { get; set; }
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public decimal OrderPrice { get; set; }
}