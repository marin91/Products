using Domain.Abstractions;
using Domain.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the product inventory service in the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to which the service is added.</param>
        /// <returns>The updated IServiceCollection instance.</returns>
        public static IServiceCollection RegisterTheProductInventory(this IServiceCollection services)
        {
            return services.AddScoped<IProductInventory, ProductInventory>();
        }

    }
}
