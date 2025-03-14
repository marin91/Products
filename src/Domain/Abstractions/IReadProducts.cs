using Domain.Models;

namespace Domain.Abstractions
{
    /// <summary>
    /// Defines operations for reading product data.
    /// </summary>
    public interface IReadProducts
    {
        /// <summary>
        /// Retrieves all products available in the store.
        /// </summary>
        /// <returns>An enumerable collection of products.</returns>
        Task<IEnumerable<Product>> RetrieveAllStoreProductsAsync();

        /// <summary>
        /// Retrieves a store product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The product corresponding to the given identifier.</returns>
        Task<Product> GetStoreProductByIdAsync(int id);
    }
}
