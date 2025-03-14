using Domain.Abstractions.Repositories;
using Infrastructure.SqlServer.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductRemover : ProductRepository, IRemoveProducts
    {
        private readonly ILogger<ProductRemover> _logger;

        public ProductRemover(IOptions<ProductsConnectionOptions> connectionOptions, 
            ILogger<ProductRemover> logger) : base(connectionOptions) 
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public Task DeleteProductAsync(long productId)
        {
            throw new NotImplementedException();
        }
    }
}
