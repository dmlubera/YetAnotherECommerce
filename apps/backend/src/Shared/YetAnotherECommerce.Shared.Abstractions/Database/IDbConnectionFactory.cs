using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Abstractions.Database;

public interface IDbConnectionFactory
{
    Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken = default);
}