using Microsoft.AspNetCore.Mvc;
using ToolsBazaar.Domain.ProductAggregate;

namespace ToolsBazaar.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase {

    private readonly IProductRepository _productRepository;
    private readonly ILogger<CustomersController> _logger;
    public ProductsController(ILogger<CustomersController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    [HttpGet("most-expensive")]
    public List<Product> Get()
    {
        _logger.LogInformation($"All the products sorted by highest price first...");
        var result = _productRepository.GetAll().OrderByDescending(x => x.Price);
        return result.OrderBy(x => x.Name).ToList();

    }

}

