using Microsoft.AspNetCore.Mvc;
using Test.RabbitMq.Shop.Core.Entities;
using Test.RabbitMq.Shop.Core.Interfaces;

namespace Test.RabbitMq.Shop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductRepository _productRepository;

    public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        return Ok(_productRepository.GetProducts());
    }
    
    [HttpGet("id")]
    public ActionResult<Product> Get(int id)
    {
        var product = _productRepository.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }
        
        return Ok(product);
    }
}