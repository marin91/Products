using Infrastructure.SqlServer.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.SqlServer.Implementations.Repositories
{
    internal abstract class ProductRepositoryBase
    {
        protected readonly string _connectionString;

        internal ProductRepositoryBase(IOptions<ProductsConnectionOptions> connectionOptions) 
        { 
            _connectionString  = connectionOptions.Value.ConnectionString;
        }
    }
}
