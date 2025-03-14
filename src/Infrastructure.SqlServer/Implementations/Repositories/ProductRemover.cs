using Domain.Abstractions.Repositories;
using Infrastructure.SqlServer.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductRemover : ProductRepository, IRemoveProducts
    {
        public ProductRemover(IOptions<ProductsConnectionOptions> connectionOptions) : base(connectionOptions) 
        {

        }

        /// <inheritdoc />
        public Task DeleteProductAsync(long productId)
        {
            throw new NotImplementedException();
        }
    }
}
