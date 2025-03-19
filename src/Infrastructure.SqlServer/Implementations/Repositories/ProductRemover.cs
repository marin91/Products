using Domain.Abstractions.Repositories;
using Domain.Exceptions;
using Infrastructure.SqlServer.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductRemover : ProductRepository, IRemoveProducts
    {
        private readonly ILogger<ProductRemover> _logger;

        public ProductRemover(IOptions<ProductsConnectionOptions> connectionOptions, 
            ILogger<ProductRemover> logger) : base(connectionOptions) 
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task DeleteProductAsync(long productId)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete a product with Id: {productId}");

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string sql = "DELETE FROM Product WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", productId);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected == 0)
                        {
                            throw new ProductDoesNotExistException(productId);
                        }

                        _logger.LogInformation("The product was successfully deleted from the Product table.");
                    }

                }
            }
            catch (ProductDoesNotExistException ex)
            {
                _logger.LogError(ex, "The product was expected to be persisted in the table.");

                throw;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error was raised from the SQL Server engine.");

                throw;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An unexpected issue occurred while retrieving a product by it's Id.");

                throw;
            }
        }
    }
}
