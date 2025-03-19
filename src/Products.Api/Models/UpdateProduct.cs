namespace Products.Api.Models
{
    public class UpdateProduct : Product
    {
        /// <summary>
        /// The current unique identifier of the product.
        /// </summary>
        public long CurrentId { get; set; }
    }
}
