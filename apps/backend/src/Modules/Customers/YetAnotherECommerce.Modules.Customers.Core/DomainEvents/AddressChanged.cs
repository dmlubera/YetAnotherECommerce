using YetAnotherECommerce.Modules.Customers.Core.Entities;
using YetAnotherECommerce.Modules.Customers.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Customers.Core.DomainEvents;

public record AddressChanged(User User, Address Address) : IDomainEvent;