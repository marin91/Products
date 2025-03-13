using Domain.Abstractions;
using Products.Api.Models;
using DomainProduct = Domain.Models.Product;


namespace Products.Api.Mappers
{
    /// <summary>
    /// Maps a <see cref="Product"/> instance to a <see cref="DomainProduct"/> instance.
    /// </summary>
    public class ProductMapper : IMap<Product, DomainProduct>
    {
        /// <inheritdoc />
        public DomainProduct Map(Product source)
        {
            return new DomainProduct(source.Id, source.Description, source.Price, source.Quantity);
        }
    }
}
