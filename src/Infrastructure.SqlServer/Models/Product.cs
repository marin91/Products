namespace Infrastructure.SqlServer.Models
{
    internal class Product
    {
        /// <summary>
        /// The unique identifier of the product.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The product's description.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// The current retail price of the item.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The current amount of units available in the store.
        /// </summary>
        public int Quantity { get; set; }
    }
}
