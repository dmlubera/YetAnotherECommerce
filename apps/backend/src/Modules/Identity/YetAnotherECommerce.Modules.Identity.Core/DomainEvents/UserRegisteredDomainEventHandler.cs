using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.Notifications;

namespace YetAnotherECommerce.Modules.Identity.Core.DomainEvents;

public class UserRegisteredDomainEventHandler(
    IIdentityMessagePublisher messagePublisher,
    INotificationSender notificationSender) : IDomainEventHandler<UserRegistered>
{
    public async Task HandleAsync(UserRegistered @event)
    {
        await messagePublisher.PublishAsync(new Events.UserRegistered(@event.Id, @event.Email));
        await notificationSender.SendAsync(
            new YetAnotherECommerce.Shared.Contracts.Notifications.Identity.UserRegistered(@event.Email));
    }
}