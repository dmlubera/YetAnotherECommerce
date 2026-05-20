using YetAnotherECommerce.Modules.Customers.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Customers.Core.DomainEvents;

public record UserCreated(User User) : IDomainEvent;