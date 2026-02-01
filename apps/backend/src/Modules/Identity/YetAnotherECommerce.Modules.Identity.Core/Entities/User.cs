using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using YetAnotherECommerce.Modules.Identity.Core.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Identity.Core.Entities;

public class User : IdentityUser<Guid>, IHasDomainEvents
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    public void MarkAsRegistered()
    {
        AddDomainEvent(new UserRegistered(Id, Email));
    }
    
    public void ClearDomainEvents() => _domainEvents.Clear();
    private void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}