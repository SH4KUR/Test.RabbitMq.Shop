using Test.RabbitMq.Shop.Api.Models;
using Test.RabbitMq.Shop.Core.Entities;

namespace Test.RabbitMq.Shop.Api.Helpers;

public static class EntityMapper
{
    public static OrderModel MapOrderToModel(Order order, Product? product)
    {
        if (product == null || order.ProductId != product.Id)
        {
            throw new InvalidDataException();
        }

        return new OrderModel
        {
            Id = order.Id,
            Product = MapProductToModel(product),
            ProductQuantity = order.ProductQuantity,
            OrderPrice = order.OrderPrice,
            CreationDateTime = order.CreationDateTime
        };
    }
    
    public static ProductModel MapProductToModel(Product product)
    {
        return new ProductModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description
        };
    }
}