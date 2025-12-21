using YetAnotherECommerce.Modules.Users.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Users.Core.DomainEvents;

public record LastNameChanged(User User, string LastName) : IDomainEvent;