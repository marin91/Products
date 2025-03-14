namespace Infrastructure.SqlServer.Options
{
    /// <summary>
    /// Represents the configuration options for connecting to the product database.
    /// </summary>
    public class ProductsConnectionOptions
    {
        /// <summary>
        /// The configuration section name used to retrieve database connection settings.
        /// </summary>
        public const string ConfigSection = "ProductDatabaseConnection";

        /// <summary>
        /// Gets or sets the connection string used to connect to the product database.
        /// </summary>
        public string ConnectionString { get; set; } = default!;
    }
}
