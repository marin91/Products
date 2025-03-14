using Domain.Abstractions;
using Domain.Models;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductReader : IReadProducts
    {
        /// <inheritdoc />
        public Product GetStoreProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<Product> RetrieveAllStoreProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
