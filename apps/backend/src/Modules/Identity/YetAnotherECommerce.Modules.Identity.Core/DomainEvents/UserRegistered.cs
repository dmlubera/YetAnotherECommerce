using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Identity.Core.DomainEvents;

public record UserRegistered(User User) : IDomainEvent;