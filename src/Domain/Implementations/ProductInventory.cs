using Domain.Abstractions;
using Domain.Abstractions.Repositories;
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
        public async Task DeleteProductAsync(Product product)
        {
            try
            {
                _logger.LogInformation($"Attempting to remove a product from the system with the following Id: {product.Id}.");

                await _productsRemover.DeleteProductAsync(product.Id);

                _logger.LogInformation($"The product was successfully removed from the system.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred when removing a product from the system.");

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
                _logger.LogError(ex, "An unknown error occurred when retrieving the products.");

                throw;
            }            
        }

        /// <inheritdoc />
        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                _logger.LogInformation("Attempting to update an existing product in the system.");

                await _productsWriter.UpdateProductAsync(product);

                _logger.LogInformation("The product was successfully updated.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred while updating the product in the system.");

                throw;
            }
        }
    }
}
