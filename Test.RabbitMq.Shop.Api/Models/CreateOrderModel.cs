namespace Test.RabbitMq.Shop.Api.Models;

public class CreateOrderModel
{
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }
}