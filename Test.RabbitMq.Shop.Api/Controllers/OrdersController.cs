using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test.RabbitMq.Shop.Api.Helpers;
using Test.RabbitMq.Shop.Api.Models;
using Test.RabbitMq.Shop.Common.Messages.Events;
using Test.RabbitMq.Shop.Core.Entities;
using Test.RabbitMq.Shop.Core.Interfaces;

namespace Test.RabbitMq.Shop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrdersController(ILogger<OrdersController> logger, IOrderRepository orderRepository, IProductRepository productRepository, IPublishEndpoint publishEndpoint)
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
    public async Task<ActionResult> Post(CreateOrderModel model)
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
            OrderPrice = product.Price * model.ProductQuantity,
            ProductQuantity = model.ProductQuantity,
            CreationDateTime = DateTime.Now
        };
        await _orderRepository.AddOrderAsync(newOrder);
        
        _logger.LogInformation($"Order {newOrder.Id} added");

        var message = new OrderCreatedEvent(
            newOrder.Id,
            product.Id,
            newOrder.OrderPrice);
        
        await _publishEndpoint.Publish(message);
        
        _logger.LogWarning($"OrderCreatedEvent message published: {JsonConvert.SerializeObject(message)}");
        
        return Ok();
    }
}