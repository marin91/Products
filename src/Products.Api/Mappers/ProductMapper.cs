using Domain.Abstractions;
using Products.Api.Models;
using DomainProduct = Domain.Models.Product;


namespace Products.Api.Mappers
{
    /// <summary>
    /// Maps a <see cref="Product"/> instance to / from <see cref="DomainProduct"/> instance.
    /// </summary>
    public class ProductMapper : IMap<Product, DomainProduct>, IMap<DomainProduct, Product>
    {
        /// <inheritdoc />
        public DomainProduct Map(Product source)
        {
            return new DomainProduct(source.Id, source.Description, source.Price, source.Quantity);
        }

        /// <inheritdoc />
        public Product Map(DomainProduct source)
        {
            return new Product
            {
                Id = source.Id,
                Description = source.Description,
                Price = source.Price,
                Quantity = source.Quantity
            };
        }
    }
}
