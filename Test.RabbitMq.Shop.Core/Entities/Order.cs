namespace Test.RabbitMq.Shop.Core.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public decimal OrderPrice { get; set; }
    public DateTime CreationDateTime { get; set; }
}