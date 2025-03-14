using Infrastructure.SqlServer.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal abstract class ProductRepository
    {
        protected readonly string _connectionString;

        internal ProductRepository(IOptions<ProductsConnectionOptions> connectionOptions) 
        { 
            _connectionString  = connectionOptions.Value.ConnectionString;
        }
    }
}
