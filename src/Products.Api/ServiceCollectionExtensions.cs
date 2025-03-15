using Domain.Abstractions;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Products.Api.Mappers;
using Products.Api.Models;
using Products.Api.Validators;
using System.Net;
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
        public static IServiceCollection RegisterTheProductMappers(this IServiceCollection services)
        {
            return services.AddScoped<IMap<Product, DomainProduct>, ProductMapper>()
                .AddScoped<IMap<DomainProduct, Product>, ProductMapper>();
        }

        /// <summary>
        /// Registers the <see cref="ProductValidator"/> instance in the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to which the service is added.</param>
        /// <returns>The updated IServiceCollection instance.</returns>
        public static IServiceCollection RegisterTheProductValidator(this IServiceCollection services)
        {
            return services.AddScoped<IValidator<Product>, ProductValidator>();
        }

        public static IServiceCollection RegisterProblemDetailsHandling(this IServiceCollection services)
        {
            return services.AddProblemDetails(options =>
            {

                // Handle validation exceptions (e.g., FluentValidation)
                options.Map<ValidationException>((ctx, ex) =>
                {
                    var logger = ctx.RequestServices.GetRequiredService<ILogger<Program>>();

                    logger.LogWarning(ex, "Validation failed for request to {Path}", ctx.Request.Path);

                    var validationProblemDetails = new ValidationProblemDetails
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Title = "Validation Error",
                        Detail = "One or more validation errors occurred.",
                        Type = $"{ctx.Request.Scheme}://{ctx.Request.Host}/errors/validation-error",
                        Instance = ctx.Request.Path
                    };

                    // If using FluentValidation, add validation errors to the response
                    if (ex is ValidationException validationEx)
                    {
                        foreach (var error in validationEx.Errors)
                        {
                            if (!validationProblemDetails.Errors.ContainsKey(error.PropertyName))
                            {
                                validationProblemDetails.Errors[error.PropertyName] =
                                    new[] { error.ErrorMessage };
                            }
                        }
                    }

                    return validationProblemDetails;
                });
            });
        }
    }
}
