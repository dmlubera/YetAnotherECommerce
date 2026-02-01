using System;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Identity.Core.DomainEvents;

public record UserRegistered(Guid Id, string Email) : IDomainEvent;