using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Products.Api.Models;
using DomainProduct = Domain.Models.Product;

namespace Products.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IProductInventory _productInventory;

        private readonly IMap<Product, DomainProduct> _productMapper;

        public ProductController(ILogger<ProductController> logger, IProductInventory productInventory, 
            IMap<Product, DomainProduct> productMapper)
        {
            _logger = logger;
            _productInventory = productInventory;
            _productMapper = productMapper;
        }

        /// <summary>Retrieves all of the products in the system.</summary>
        /// <response code="200">Successfully retrieved all of the products.</response>
        /// <response code="500">An unexpected internal server error occurred while processing the request.</response>        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAllStoreProducts()
        {

            _logger.LogInformation("Attempting to retrieve all of the store products.");

            try
            {
                var storeProducts = await _productInventory.RetrieveAllProductsAsync();

                _logger.LogInformation("The store products were successfully retrieved.");

                return Ok(storeProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while retrieving all of the store's products.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>Creates a product in the system.</summary>
        /// <param name="product">The product to be added.</param>
        /// <response code="201">Successfully created the product and added it to the system.</response>
        /// <response code="400">The request contains invalid or missing data.</response>
        /// <response code="500">An unexpected internal server error occurred while processing the request.</response>        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {

            _logger.LogInformation("Attempting to add a product.");

            try
            {
                var domainProduct = _productMapper.Map(product);

                await _productInventory.AddProductAsync(domainProduct);

                _logger.LogInformation("The product was successfully added.");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while adding a product to the system.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }


        /// <summary>
        /// Updates an existing product in the inventory system.
        /// </summary>
        /// <response code="204">Successfully updated the product.</response>
        /// <response code="400">The request contains invalid or missing data.</response>
        /// <response code="404">The product does not exist in the system.</response>
        /// <response code="500">An unexpected internal server error occurred while processing the request.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task <IActionResult> UpdateProduct([FromBody] Product product)
        {

            _logger.LogInformation("Attempting to update a product.");

            try
            {
                var domainProduct = _productMapper.Map(product);

                await _productInventory.UpdateProductAsync(domainProduct);

                _logger.LogInformation("The product was successfully updated.");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while updating an existing product.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes an existing product from the inventory system.
        /// </summary>
        /// <response code="204">The product was successfully deleted.</response>
        /// <response code="400">The request contains invalid or missing data.</response>
        /// <response code="404">The product does not exist in the system.</response>
        /// <response code="500">An unexpected internal server error occurred while processing the request.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("/[controller]/{productId:int}")]
        public IActionResult DeleteProduct(long productId)
        {

            _logger.LogInformation($"Attempting to delete a product with the following id: {productId}");

            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while deleting the product from the system.");
               
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }
    }
}
