using Domain.Abstractions;
using Infrastructure.SqlServer.Models;
using DomainProduct = Domain.Models.Product;


namespace Infrastructure.SqlServer.Mappers
{
    internal class ProductMapper : IMap<Product, DomainProduct>
    {
        public DomainProduct Map(Product source)
        {
            return new DomainProduct(source.Id, 
                source.Description, 
                source.Price, 
                source.Quantity);
        }
    }
}
