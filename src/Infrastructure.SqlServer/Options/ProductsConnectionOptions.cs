namespace Infrastructure.SqlServer.Options
{
    public class ProductsConnectionOptions
    {
        public const string ConfigSection = "ProductDatabaseConnection";

        public string ConnectionString { get; set; } = default!;
    }
}
