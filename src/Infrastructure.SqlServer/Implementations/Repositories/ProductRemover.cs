using Domain.Abstractions.Repositories;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductRemover : IRemoveProducts
    {
        /// <inheritdoc />
        public Task DeleteProductAsync(long productId)
        {
            throw new NotImplementedException();
        }
    }
}
