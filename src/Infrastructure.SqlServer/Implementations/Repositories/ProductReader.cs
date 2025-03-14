using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Infrastructure.SqlServer.Models;
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
