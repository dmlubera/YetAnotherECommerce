using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Abstractions.Outbox;

public interface IProcessOutboxJob
{
    Task ProcessAsync();
}