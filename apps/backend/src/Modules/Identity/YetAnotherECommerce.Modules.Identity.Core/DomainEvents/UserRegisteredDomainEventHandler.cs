using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Modules.Identity.Core.DomainEvents;

public class UserRegisteredDomainEventHandler(IIdentityMessagePublisher messagePublisher) : IDomainEventHandler<UserRegistered>
{
    public async Task HandleAsync(UserRegistered @event)
    {
        await messagePublisher.PublishAsync(new Events.UserRegistered(@event.Id, @event.Email));
    }
}