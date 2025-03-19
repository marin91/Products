using Domain.Abstractions.Repositories;
using Domain.Exceptions;
using Infrastructure.SqlServer.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DomainProduct = Domain.Models.Product;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal class ProductWriter : ProductRepository, IWriteProducts
    {
        private readonly ILogger<ProductWriter> _logger;

        public ProductWriter(IOptions<ProductsConnectionOptions> connectionOptions, 
            ILogger<ProductWriter> logger) : base(connectionOptions) 
        { 
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task CreateProductAsync(DomainProduct product)
        {            
            try
            {
                var productId = product.Id;

                _logger.LogInformation($"Attempting to create a product with Id: {productId}");

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string sql = "INSERT INTO Product (Id, Description, Price, Quantity) VALUES (@Id, @Description, @Price, @Quantity)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", product.Id);
                        cmd.Parameters.AddWithValue("@Description", product.Description);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Quantity", product.Quantity);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("The query executed successfully but the product was not added to the table.");
                        }

                        _logger.LogInformation("The product was successfully created in the Product table.");
                    }

                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error was raised from the SQL Server engine.");

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected issue occurred while creating a product.");

                throw;
            }
        }

        /// <inheritdoc />
        public async Task UpdateProductAsync(long currentId, DomainProduct product)
        {
            try
            {
                _logger.LogInformation($"Attempting to update a product with Id: {currentId}");

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string sql = "UPDATE Product SET Id = @Id, Description = @Description, Price = @Price, Quantity = @Quantity WHERE Id = @CurrentId";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CurrentId", currentId);
                        cmd.Parameters.AddWithValue("@Id", product.Id);
                        cmd.Parameters.AddWithValue("@Description", product.Description);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Quantity", product.Quantity);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("The query executed successfully but the product was not updated.");
                        }

                        _logger.LogInformation("The product was successfully updated.");
                    }

                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error was raised from the SQL Server engine.");

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected issue occurred while updating a product.");

                throw;
            }
        }

        /// <inheritdoc />
        public async Task UpdateProductAsync(DomainProduct product)
        {
            try
            {
                var productId = product.Id;

                _logger.LogInformation($"Attempting to update a product with Id: {productId}");

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string sql = "UPDATE Product SET Description = @Description, Price = @Price, Quantity = @Quantity WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", product.Id);
                        cmd.Parameters.AddWithValue("@Description", product.Description);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Quantity", product.Quantity);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("The query executed successfully but the product was not updated.");
                        }

                        _logger.LogInformation("The product was successfully updated.");
                    }

                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error was raised from the SQL Server engine.");

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected issue occurred while updating a product.");

                throw;
            }
        }
    }
}
