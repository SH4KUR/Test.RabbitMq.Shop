using Microsoft.AspNetCore.Mvc;
using Test.RabbitMq.Shop.Api.Helpers;
using Test.RabbitMq.Shop.Api.Models;
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
    public ActionResult<IEnumerable<ProductModel>> Get()
    {
        var model = _productRepository
            .GetProducts()
            .OrderBy(p => p.Id)
            .Select(EntityMapper.MapProductToModel);
        
        return Ok(model);
    }

    [HttpGet("id")]
    public ActionResult<ProductModel> Get(int id)
    {
        var product = _productRepository.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }
        
        return Ok(EntityMapper.MapProductToModel(product));
    }
}