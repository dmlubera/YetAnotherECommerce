using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Abstractions.Events;

public interface IEventDispatcher
{
    Task DispatchAsync(IEvent @event);
}