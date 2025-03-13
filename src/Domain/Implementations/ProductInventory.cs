using Domain.Abstractions;
using Domain.Models;

namespace Domain.Implementations
{
    internal class ProductInventory : IProductInventory
    {
        /// <inheritdoc />
        public Task AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task DeleteProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IEnumerable<Product>> RetrieveAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
