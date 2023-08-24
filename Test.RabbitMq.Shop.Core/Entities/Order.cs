namespace Test.RabbitMq.Shop.Core.Entities;

public class Order
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public decimal OrderPrice { get; set; }
    public DateTime CreationDateTime { get; set; }
}