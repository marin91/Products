using Domain.Models;

namespace Domain.Abstractions
{
    internal interface IReadProducts
    {
        public IEnumerable<Product> RetrieveAllStoreProductsAsync();

        public Product GetStoreProductByIdAsync(int id);
    }
}
