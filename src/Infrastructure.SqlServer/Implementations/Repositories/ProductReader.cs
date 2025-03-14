using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Infrastructure.SqlServer.Models;
using Infrastructure.SqlServer.Options;
using Microsoft.Extensions.Options;
using DomainProduct = Domain.Models.Product;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductReader : ProductRepository, IReadProducts
    {
        private readonly IMap<Product, DomainProduct> _productMapper;

        public ProductReader(IOptions<ProductsConnectionOptions> connectionOptions, 
            IMap<Product, DomainProduct> productMapper) : base(connectionOptions) 
        { 
            _productMapper = productMapper;
        }

        /// <inheritdoc />
        public Task<DomainProduct> GetStoreProductByIdAsync(int id)
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
