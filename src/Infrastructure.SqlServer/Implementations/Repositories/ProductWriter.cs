using Domain.Abstractions.Repositories;
using Domain.Models;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductWriter : IWriteProducts
    {
        /// <inheritdoc />
        public Task CreateProductAsync(Product product)
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
