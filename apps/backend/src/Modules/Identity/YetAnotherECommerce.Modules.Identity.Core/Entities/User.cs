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
    
    private User() {}

    public static User Register(string email)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            UserName = email
        };
        user.AddDomainEvent(new UserRegistered(user.Id,  user.Email));
        return user;
    }
    
    public void ClearDomainEvents() => _domainEvents.Clear();
    private void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}