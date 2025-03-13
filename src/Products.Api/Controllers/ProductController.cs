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

            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while retrieving all of the store's products.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(allProducts);
        }

        [HttpPost(Name = "AddProduct")]
        public IActionResult AddProduct([FromBody] Product product)
        {

            _logger.LogInformation("Atttempting to add a product.");

            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while adding a product to the system.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpPut(Name = "UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] Product product)
        {

            _logger.LogInformation("Atttempting to update a product.");

            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while updating an existing product.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpPut(Name = "DeleteProduct")]
        public IActionResult DeleteProduct([FromBody] long productId)
        {

            _logger.LogInformation($"Atttempting to delete a product with the following id: {productId}");

            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while deleting the product from the system.");
               
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


            return Ok();
        }
    }
}
