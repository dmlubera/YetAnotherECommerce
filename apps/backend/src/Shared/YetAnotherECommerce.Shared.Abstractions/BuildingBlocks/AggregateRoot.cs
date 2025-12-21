using System.Collections.Generic;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

public class AggregateRoot
{
    private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
    public AggregateId Id { get; protected set; }
    public int Version { get; private set; }
    public IEnumerable<IDomainEvent> Events => _events;

    protected void AddEvent(IDomainEvent @event)
    {
        _events.Add(@event);
        Version++;
    }

    protected void IncrementVersion()
        => Version++;
}