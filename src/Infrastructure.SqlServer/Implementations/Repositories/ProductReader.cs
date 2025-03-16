using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Infrastructure.SqlServer.Models;
using Infrastructure.SqlServer.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DomainProduct = Domain.Models.Product;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductReader : ProductRepository, IReadProducts
    {
        private readonly ILogger<ProductReader> _logger;

        private readonly IMap<Product, DomainProduct> _productMapper;

        public ProductReader(IOptions<ProductsConnectionOptions> connectionOptions, 
            IMap<Product, DomainProduct> productMapper, ILogger<ProductReader> logger) : base(connectionOptions) 
        { 
            _productMapper = productMapper;
            _logger = logger;
        }

        /// <inheritdoc />
        public Task<DomainProduct> GetStoreProductByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IEnumerable<DomainProduct>> RetrieveAllStoreProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
