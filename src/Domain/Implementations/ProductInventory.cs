using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Domain.Implementations
{
    internal class ProductInventory : IProductInventory
    {
        private readonly ILogger<ProductInventory> _logger;

        private readonly IReadProducts _productsReader;

        private readonly IWriteProducts _productsWriter;

        private readonly IRemoveProducts _productsRemover;

        public ProductInventory(IReadProducts productsReader, 
            IWriteProducts productsWriter, 
            IRemoveProducts productsRemover, 
            ILogger<ProductInventory> logger)
        {
            _productsReader = productsReader;
            _productsWriter = productsWriter;
            _productsRemover = productsRemover;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task AddProductAsync(Product product)
        {
            try
            {
                _logger.LogInformation("Attempting to create product in the system.");

                await _productsWriter.CreateProductAsync(product);

                _logger.LogInformation("The product was successfully created in the system.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while creating the product in the system.");

                throw;
            }
        }

        /// <inheritdoc />
        public async Task DeleteProductAsync(long productId)
        {
            try
            {
                _logger.LogInformation($"Attempting to remove a product from the system with the following Id: {productId}.");

                await ThrowExceptionIfProductDoesNotExistInSystem(productId);

                await _productsRemover.DeleteProductAsync(productId);

                _logger.LogInformation($"The product was successfully removed from the system.");

            }
            catch (ProductDoesNotExistException ex)
            {
                _logger.LogError(ex, "The product can't be removed because it doesn't exist in the system.");

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while removing a product from the system.");

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Product>> RetrieveAllProductsAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all of the store products.");

                var allProducts = await _productsReader.RetrieveAllStoreProductsAsync();

                _logger.LogInformation($"The products were obtained successfully. Product Count: {allProducts.Count()}");

                return allProducts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while retrieving the products.");

                throw;
            }            
        }

        /// <inheritdoc />
        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                var productId = product.Id;

                _logger.LogInformation("Attempting to update an existing product in the system.");

                await ThrowExceptionIfProductDoesNotExistInSystem(productId);

                await _productsWriter.UpdateProductAsync(product);

                _logger.LogInformation("The product was successfully updated.");

            }
            catch (ProductDoesNotExistException ex)
            {
                _logger.LogError(ex, "The product can't be updated because it doesn't exist in the system.");

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while updating the product in the system.");

                throw;
            }
        }

        private async Task ThrowExceptionIfProductDoesNotExistInSystem(long productId)
        {
            var dbProduct = await _productsReader.GetStoreProductByIdAsync(productId);

            if (dbProduct is null)
            {
                throw new ProductDoesNotExistException(productId);
            }
        }
    }
}
