namespace Test.RabbitMq.Shop.Common.Messages;

public class OrderCreatedEvent
{
    public OrderCreatedEvent(Guid productId, int productQuantity, decimal orderPrice)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ProductQuantity = productQuantity;
        OrderPrice = orderPrice;
        EventDateTime = DateTime.Now;
    }

    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public decimal OrderPrice { get; set; }
    public DateTime EventDateTime { get; set; }
}