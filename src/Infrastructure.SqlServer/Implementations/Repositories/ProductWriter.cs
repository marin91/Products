using Domain.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.SqlServer.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductWriter : ProductRepository, IWriteProducts
    {
        private readonly ILogger<ProductWriter> _logger;

        public ProductWriter(IOptions<ProductsConnectionOptions> connectionOptions, 
            ILogger<ProductWriter> logger) : base(connectionOptions) 
        { 
            _logger = logger;
        }

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
