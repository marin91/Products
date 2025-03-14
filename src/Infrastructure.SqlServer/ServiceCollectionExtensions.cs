using Domain.Abstractions;
using Infrastructure.SqlServer.Mappers;
using Infrastructure.SqlServer.Models;
using Microsoft.Extensions.DependencyInjection;
using DomainProduct = Domain.Models.Product;

namespace Infrastructure.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the product db mapper in the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to which the service is added.</param>
        /// <returns>The updated IServiceCollection instance.</returns>
        public static IServiceCollection RegisterDbProductMapper(this IServiceCollection services)
        {
            return services.AddScoped<IMap<Product, DomainProduct>, ProductMapper>();
        }
    }
}
