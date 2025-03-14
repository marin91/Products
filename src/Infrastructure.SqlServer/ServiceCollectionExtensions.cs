using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Infrastructure.SqlServer.Implementations.Repositories;
using Infrastructure.SqlServer.Mappers;
using Infrastructure.SqlServer.Models;
using Microsoft.Extensions.DependencyInjection;
using DomainProduct = Domain.Models.Product;

namespace Infrastructure.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers product repository services in the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection to add the repositories to.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection RegisterProductRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IReadProducts, ProductReader>()
                .AddScoped<IRemoveProducts, ProductRemover>()
                .AddScoped<IWriteProducts, ProductWriter>()
                .RegisterDbProductMapper();
        }

        /// <summary>
        /// Registers the product db mapper in the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to which the service is added.</param>
        /// <returns>The updated IServiceCollection instance.</returns>
        private static IServiceCollection RegisterDbProductMapper(this IServiceCollection services)
        {
            return services.AddScoped<IMap<Product, DomainProduct>, ProductMapper>();
        }

    }
}
