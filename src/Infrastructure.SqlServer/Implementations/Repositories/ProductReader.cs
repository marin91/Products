using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Models;
using DomainProduct = Domain.Models.Product;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductReader : IReadProducts
    {
        private readonly IMap<Product, DomainProduct> _productMapper;

        public ProductReader(IMap<Product, DomainProduct> productMapper) 
        { 
            _productMapper = productMapper;
        }

        /// <inheritdoc />
        public Task<Product> GetStoreProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IEnumerable<Product>> RetrieveAllStoreProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
