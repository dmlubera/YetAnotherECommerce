using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent @event);
}