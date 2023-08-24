using MassTransit;
using Microsoft.AspNetCore.Mvc;
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
    public ActionResult<IEnumerable<Order>> Get()
    {
        return Ok(_orderRepository
            .GetOrders()
            .OrderByDescending(o => o.CreationDateTime));
    }
    
    [HttpPost]
    public ActionResult Post(Order order)
    {
        var product = _productRepository.GetProduct(order.ProductId);
        if (product == null)
        {
            return NotFound();
        }
        
        _orderRepository.AddOrder(order);
        
        _publishEndpoint.Publish(new OrderCreatedEvent(
            Guid.NewGuid(), 
            order.Id,
            product.Id, 
            order.ProductQuantity, 
            order.OrderPrice));
        
        return Ok();
    }
}