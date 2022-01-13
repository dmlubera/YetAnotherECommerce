using YetAnotherECommerce.Modules.Users.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Users.Core.DomainEvents
{
    public record FirstNameChanged(User User, string FirstName) : IDomainEvent;
}