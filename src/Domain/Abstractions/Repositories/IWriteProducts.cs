using Domain.Models;

namespace Domain.Abstractions.Repositories
{
    /// <summary>
    /// Defines operations for writing product data.
    /// </summary>
    public interface IWriteProducts
    {
        /// <summary>
        /// Asynchronously creates a new product.
        /// </summary>
        /// <param name="product">The product to be created.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateProductAsync(Product product);

        /// <summary>
        /// Asynchronously updates an existing product.
        /// </summary>
        /// <param name="product">The product with updated information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateProductAsync(Product product);

        /// <summary>
        /// Asynchronously updates an existing product, along with the Id.
        /// </summary>
        /// <param name="currentId">The product's current unique Id.</param>
        /// <param name="product">The product with updated information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateProductAsync(long currentId, Product product);

    }
}
