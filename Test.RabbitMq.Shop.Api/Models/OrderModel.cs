using System.Data.Common;

namespace Test.RabbitMq.Shop.Api.Models;

public class OrderModel
{
    public Guid Id { get; set; }
    public ProductModel Product { get; set; }
    public int ProductQuantity { get; set; }
    public decimal OrderPrice { get; set; }
    public DateTime CreationDateTime { get; set; }
}