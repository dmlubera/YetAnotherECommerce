using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Notifications;
using YetAnotherECommerce.Shared.Infrastructure.Settings;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ResendEmailConfirmation;

public class ResendEmailConfirmationCommandHandler(
    IOptions<ClientAppSettings> options,
    INotificationSender notificationSender,
    UserManager<User> userManager) : ICommandHandler<ResendEmailConfirmationCommand, ResendEmailConfirmationResult>
{
    private readonly ClientAppSettings _clientAppSettings = options.Value;

    public async Task<ResendEmailConfirmationResult> HandleAsync(ResendEmailConfirmationCommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if (user is not { EmailConfirmed: false }) return ResendEmailConfirmationResult.Succeeded();
        
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var emailConfirmationLink =
            $"{_clientAppSettings.BaseUrl}{_clientAppSettings.Paths.EmailConfirmation}?token={WebUtility.UrlEncode(token)}&userId={user.Id}";
        await notificationSender.SendAsync(
            new YetAnotherECommerce.Shared.Contracts.Notifications.Identity.UserRegistered(user.Email!,
                emailConfirmationLink));

        return ResendEmailConfirmationResult.Succeeded();
    }
}