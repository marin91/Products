using Microsoft.AspNetCore.Mvc;
using Product.Api.Models;

namespace Product.Api.Controllers
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
    }
}
