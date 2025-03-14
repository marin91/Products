using Domain.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.SqlServer.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductWriter : ProductRepository, IWriteProducts
    {
        public ProductWriter(IOptions<ProductsConnectionOptions> connectionOptions) : base(connectionOptions) 
        { 
        
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
