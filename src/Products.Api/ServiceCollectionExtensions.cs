using Domain.Abstractions;
using Products.Api.Mappers;
using Products.Api.Models;
using DomainProduct = Domain.Models.Product;

namespace Products.Api
{
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the product mapper in the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to which the service is added.</param>
        /// <returns>The updated IServiceCollection instance.</returns>
        public static IServiceCollection RegisterTheProductMapper(this IServiceCollection services)
        {
            return services.AddScoped<IMap<Product, DomainProduct>, ProductMapper>();
        }
    }
}
