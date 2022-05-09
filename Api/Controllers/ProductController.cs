using DemoApp.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductService _repo;

        public ProductController(ILogger<ProductController> logger, ProductService service)
        {
            _logger = logger;
            _repo = service;
        }

        [HttpGet]
        public async Task<List<Product>> GetAllAsync([FromHeader(Name = "Accept-Language")] string language = "nb-NO")
        {
            var products = await _repo.GetAllAsync(language);
            return products;
        }
    }
}