using Domain.Models;

namespace Domain.Abstractions
{
    /// <summary>
    /// Defines operations for managing the product inventory in the store.
    /// </summary>
    public interface IProductInventory
    {
        /// <summary>
        /// Retrieves all products from the inventory.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<IEnumerable<Product>> RetrieveAllProductsAsync();

        /// <summary>
        /// Adds a new product to the inventory.
        /// </summary>
        /// <param name="product">The product to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task AddProductAsync(Product product);

        /// <summary>
        /// Updates an existing product in the inventory.
        /// </summary>
        /// <param name="product">The product with updated details.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task UpdateProductAsync(Product product);

        /// <summary>
        /// Deletes a product from the inventory.
        /// </summary>
        /// <param name="productId">The product Id of the product to be removed.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task DeleteProductAsync(long productId);
    }

}
