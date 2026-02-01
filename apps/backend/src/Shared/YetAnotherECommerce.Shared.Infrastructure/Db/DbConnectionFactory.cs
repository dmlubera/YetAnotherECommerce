using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using YetAnotherECommerce.Shared.Abstractions.Database;

namespace YetAnotherECommerce.Shared.Infrastructure.Db;

public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public async Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}