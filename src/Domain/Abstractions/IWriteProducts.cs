﻿using Domain.Models;

namespace Domain.Abstractions
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
        /// Asynchronously deletes a product by its unique identifier.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteProductAsync(long productId);
    }
}
