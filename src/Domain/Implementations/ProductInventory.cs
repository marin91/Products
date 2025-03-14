using Domain.Abstractions;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Domain.Implementations
{
    internal class ProductInventory : IProductInventory
    {
        private readonly ILogger<ProductInventory> _logger;

        private readonly IReadProducts _productsReader;

        private readonly IWriteProducts _productsWriter;

        public ProductInventory(IReadProducts productsReader, IWriteProducts productsWriter, ILogger<ProductInventory> logger)
        {
            _productsReader = productsReader;
            _productsWriter = productsWriter;
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
        public Task DeleteProductAsync(Product product)
        {
            throw new NotImplementedException();
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
