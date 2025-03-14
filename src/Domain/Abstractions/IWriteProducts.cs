using Domain.Models;

namespace Domain.Abstractions
{
    internal interface IWriteProducts
    {
        public Task CreateProductAsync(Product product);

        public Task UpdateProductAsync(Product product);

        public Task DeleteProductAsync(long productId);

    }
}
