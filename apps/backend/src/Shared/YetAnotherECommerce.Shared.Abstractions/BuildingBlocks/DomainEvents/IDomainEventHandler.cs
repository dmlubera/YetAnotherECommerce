using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents
{
    public interface IDomainEventHandler<in TEvent> where TEvent : class, IDomainEvent
    {
        Task HandleAsync(IDomainEvent @event);
    }
}