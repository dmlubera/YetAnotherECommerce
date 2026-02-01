using System.Collections.Generic;

namespace YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

public interface IHasDomainEvents
{
    IReadOnlyCollection<IDomainEvent>  DomainEvents { get; }
    void ClearDomainEvents();
}