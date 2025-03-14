namespace Domain.Abstractions.Repositories
{
    /// <summary>
    /// Defines operations for removing product data.
    /// </summary>
    public interface IRemoveProducts
    {
        /// <summary>
        /// Asynchronously deletes a product by its unique identifier.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteProductAsync(long productId);
    }
}
