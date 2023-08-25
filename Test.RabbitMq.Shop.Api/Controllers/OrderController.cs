using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Test.RabbitMq.Shop.Api.Helpers;
using Test.RabbitMq.Shop.Api.Models;
using Test.RabbitMq.Shop.Common.Messages;
using Test.RabbitMq.Shop.Core.Entities;
using Test.RabbitMq.Shop.Core.Interfaces;

namespace Test.RabbitMq.Shop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderController(ILogger<OrderController> logger, IOrderRepository orderRepository, IProductRepository productRepository, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public ActionResult<IEnumerable<OrderModel>> Get()
    {
        var orders = _orderRepository
            .GetOrders()
            .OrderByDescending(o => o.CreationDateTime);
        var model = orders.Select(
            o => EntityMapper.MapOrderToModel(o, _productRepository.GetProduct(o.ProductId)));
            
        return Ok(model);
    }
    
    [HttpPost]
    public ActionResult Post(CreateOrderModel model)
    {
        var product = _productRepository.GetProduct(model.ProductId);
        if (product == null)
        {
            return NotFound();
        }

        var newOrder = new Order
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            ProductQuantity = model.ProductQuantity,
            OrderPrice = model.ProductQuantity * product.Price,
            CreationDateTime = DateTime.Now
        };
        _orderRepository.AddOrder(newOrder);
        
        _logger.LogInformation($"Order {newOrder.Id} added");
        
        _publishEndpoint.Publish(new OrderCreatedEvent(
            Guid.NewGuid(), 
            newOrder.Id,
            product.Id, 
            model.ProductQuantity, 
            newOrder.OrderPrice));
        
        _logger.LogInformation($"OrderCreatedEvent message published");
        
        return Ok();
    }
}