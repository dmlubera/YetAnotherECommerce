using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.Notifications;
using YetAnotherECommerce.Shared.Infrastructure.Settings;

namespace YetAnotherECommerce.Modules.Identity.Core.DomainEvents;

public class UserRegisteredDomainEventHandler(
    IOptions<ClientAppSettings> options,
    IIdentityMessagePublisher messagePublisher,
    INotificationSender notificationSender,
    UserManager<User> userManager) : IDomainEventHandler<UserRegistered>
{
    private readonly ClientAppSettings _clientAppSettings = options.Value;
    public async Task HandleAsync(UserRegistered @event)
    {
        var user = (await userManager.FindByIdAsync(@event.Id.ToString()))!;
        if (!user.EmailConfirmed)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var emailConfirmationLink =
                $"{_clientAppSettings.BaseUrl}{_clientAppSettings.Paths.EmailConfirmation}?token={WebUtility.UrlEncode(token)}&userId={user.Id}";
            await notificationSender.SendAsync(
                new YetAnotherECommerce.Shared.Contracts.Notifications.Identity.UserRegistered(@event.Email,
                    emailConfirmationLink));
        }

        await messagePublisher.PublishAsync(new Events.UserRegistered(@event.Id, @event.Email));
    }
}