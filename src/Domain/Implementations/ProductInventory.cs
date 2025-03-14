using Domain.Abstractions;
using Domain.Models;

namespace Domain.Implementations
{
    internal class ProductInventory : IProductInventory
    {
        internal readonly IReadProducts _productsReader;

        internal readonly IWriteProducts _productsWriter;

        public ProductInventory(IReadProducts productsReader, IWriteProducts productsWriter)
        {
            _productsReader = productsReader;
            _productsWriter = productsWriter;
        }

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
