namespace Domain.Models
{
    /// <summary>
    /// Represents a product item available in the store.
    /// </summary>
    public class Product
    {
        public Product(long productId, string description, decimal price, int quantity)
        {
            if (productId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(productId), "The product's Id must be larger than 0.");
            }

            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(productId), "The product's current price must be larger than 0.");
            }

            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(productId), "The product's quantity cannot be negative.");
            }

            Id = productId;
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        /// <summary>
        /// The unique identifier of the product.
        /// </summary>
        public long Id { get; init; }

        /// <summary>
        /// The product's description.
        /// </summary>
        public string Description { get; init; } = default!;

        /// <summary>
        /// The current retail price of the item.
        /// </summary>
        public decimal Price { get; init; } 

        /// <summary>
        /// The current amount of units available in the store.
        /// </summary>
        public int Quantity { get; init; }
    }
}
