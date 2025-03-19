using Domain.Abstractions;
using Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
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

        private readonly IValidator<Product> _productValidator;

        private readonly IProductInventory _productInventory;

        private readonly IMap<Product, DomainProduct> _apiToDomainMapper;

        private readonly IMap<DomainProduct, Product> _domainToApiMapper;

        public ProductController(ILogger<ProductController> logger, 
            IProductInventory productInventory, 
            IMap<Product, DomainProduct> apiToDomainMapper,
            IMap<DomainProduct, Product> domainToApiMapper,
            IValidator<Product> productValidator)
        {
            _logger = logger;
            _productInventory = productInventory;
            _apiToDomainMapper = apiToDomainMapper;
            _domainToApiMapper = domainToApiMapper;
            _productValidator = productValidator;
        }

        /// <summary>Retrieves all of the products in the system.</summary>
        /// <response code="200">Successfully retrieved all of the products.</response>
        /// <response code="204">There are currently no products in the inventory system.</response>
        /// <response code="500">An unexpected internal server error occurred while processing the request.</response>        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [HttpGet]
        public async Task<IActionResult> GetAllStoreProducts()
        {

            _logger.LogInformation("Attempting to retrieve all of the store products.");

            try
            {                
                var domainStoreProducts = await _productInventory.RetrieveAllProductsAsync();

                if(domainStoreProducts is null || !domainStoreProducts.Any())
                {
                    _logger.LogWarning("There are currently no products in the inventory system.");

                    return NoContent();
                }

                _logger.LogInformation($"{domainStoreProducts.Count()} store products were successfully retrieved.");

                var mappedProducts = domainStoreProducts.Select(_domainToApiMapper.Map);

                return Ok(mappedProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while retrieving all of the store's products.");

                throw;
            }

        }

        /// <summary>Creates a product in the system.</summary>
        /// <param name="product">The product to be added.</param>
        /// <response code="201">Successfully created the product and added it to the system.</response>
        /// <response code="400">The request contains invalid or missing data.</response>
        /// <response code="500">An unexpected internal server error occurred while processing the request.</response>        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {

            _logger.LogInformation("Attempting to add a product.");

            try
            {
                await ValidateRequestAsync(product);

                var domainProduct = _apiToDomainMapper.Map(product);

                await _productInventory.AddProductAsync(domainProduct);

                _logger.LogInformation("The product was successfully added.");

                return Ok();
            }
            catch (Exception ex) when (ex is not ValidationException)
            {
                _logger.LogError(ex, "An unknown error occurred while adding a product to the system.");

                throw;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [HttpPut]
        public async Task <IActionResult> UpdateProduct([FromBody] UpdateProduct updateProduct)
        {

            _logger.LogInformation("Attempting to update a product.");

            try
            {

                await ValidateRequestAsync(updateProduct);

                var domainProduct = _apiToDomainMapper.Map(updateProduct);

                await _productInventory.UpdateProductAsync(updateProduct.CurrentId, domainProduct);

                _logger.LogInformation("The product was successfully updated.");

                return Ok();
            }
            catch (ProductDoesNotExistException ex)
            {
                _logger.LogError(ex, "The product deletion operation did not take place because the product doesn't exist in the system.");

                throw;
            }
            catch (Exception ex) when (ex is not ValidationException) 
            {
                _logger.LogError(ex, "An unknown error occurred while updating an existing product.");

                throw;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [HttpDelete]
        [Route("/[controller]/{productId:int}")]
        public async Task<IActionResult> DeleteProduct(long productId)
        {

            _logger.LogInformation($"Attempting to delete a product with the following id: {productId}");

            try
            {
                if (productId <= 0)
                {
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure(nameof(productId), "The productId is invalid.") });
                }

                await _productInventory.DeleteProductAsync(productId);

                return NoContent();
            }
            catch(ProductDoesNotExistException ex)
            {
                _logger.LogError(ex, "The product deletion operation did not take place because the product doesn't exist in the system.");

                throw;
            }
            catch (Exception ex) when (ex is not ValidationException)
            {
                _logger.LogError(ex, "An unknown error occurred while deleting the product from the system.");
               
                throw;
            }            
        }


        private async Task ValidateRequestAsync(Product product)
        {
            var validationResult = await _productValidator.ValidateAsync(product);

            if (!validationResult.IsValid)
            {
                LogValidationErrorMessages(validationResult.Errors);

                throw new ValidationException(validationResult.Errors);
            }
        }

        private void LogValidationErrorMessages(IEnumerable<ValidationFailure> validationFailures)
        {
            var errors = validationFailures.Select(x => x.ErrorMessage).ToList();

            var formattedErrors = string.Join(", ", errors);    

            _logger.LogWarning("The request failed validation for the following reason(s): {errors}", formattedErrors);
        }
    }
}
