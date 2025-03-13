using Microsoft.AspNetCore.Mvc;
using Products.Api.Models;


namespace Products.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "All")]
        public IActionResult GetAllStoreProducts()
        {

            _logger.LogInformation("Atttempting to retrieve all of the store products.");

            var allProducts = Enumerable.Range(1, 5).Select(index => new Models.Product
            {
                Id = index,
                Description = "Some Description",
                Price = 1,
                Quantity = 10

            })
            .ToArray();

            return Ok(allProducts);
        }

        [HttpPost(Name = "AddProduct")]
        public IActionResult AddProduct([FromBody] Product product)
        {

            _logger.LogInformation("Atttempting to retrieve all of the store products.");

            var allProducts = Enumerable.Range(1, 5).Select(index => new Product
            {
                Id = index,
                Description = "Some Description",
                Price = 1,
                Quantity = 10

            })
            .ToArray();

            return Ok(allProducts);
        }
    }
}
