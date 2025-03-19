using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Infrastructure.SqlServer.Models;
using Infrastructure.SqlServer.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DomainProduct = Domain.Models.Product;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductReader : ProductRepository, IReadProducts
    {
        private readonly ILogger<ProductReader> _logger;

        private readonly IMap<Product, DomainProduct> _productMapper;

        public ProductReader(IOptions<ProductsConnectionOptions> connectionOptions, 
            IMap<Product, DomainProduct> productMapper, ILogger<ProductReader> logger) : base(connectionOptions) 
        { 
            _productMapper = productMapper;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<DomainProduct?> GetStoreProductByIdAsync(long id)
        {
            try
            {
                _logger.LogInformation($"Attempting to retrieve a product with Id: {id}");

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string sql = "SELECT Id, Description, Price, Quantity FROM Product WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return await ToProduct(reader);
                            }
                            else
                            {
                                _logger.LogWarning($"The product with Id: {id} was not found in the Products table.");

                                return null;
                            }
                        }
                    }

                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An unexpected issue occurred while retrieving a product by it's Id.");

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DomainProduct>> RetrieveAllStoreProductsAsync()
        {
            try
            {
                var allProducts = new List<DomainProduct>();

                _logger.LogInformation($"Attempting to retrieve all of the products.");

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string sql = "SELECT Id, Description, Price, Quantity FROM Product";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                             while (await reader.ReadAsync())
                            {
                                var product = await ToProduct(reader);

                                allProducts.Add(product);
                            }

                        }
                    }

                }

                return allProducts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected issue occurred while retrieving all of the products.");

                throw;
            }
        }

        private static async Task<DomainProduct> ToProduct(SqlDataReader reader)
        {
            var productId = reader.GetInt64(0);
            var description = await reader.GetTextReader(1).ReadToEndAsync();
            var price = reader.GetDecimal(2);
            var quantity = reader.GetInt32(3);

            return new DomainProduct(productId, description, price, quantity);
        }

    }
}
