using Microsoft.AspNetCore.Mvc;
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

    public OrderController(ILogger<OrderController> logger, IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Order>> Get()
    {
        return Ok(_orderRepository.GetOrders());
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
        return Ok();
    }
}