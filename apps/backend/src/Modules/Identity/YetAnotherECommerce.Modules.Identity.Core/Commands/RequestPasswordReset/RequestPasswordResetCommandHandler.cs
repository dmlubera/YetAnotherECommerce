using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Notifications;
using YetAnotherECommerce.Shared.Infrastructure.Settings;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.RequestPasswordReset;

public class RequestPasswordResetCommandHandler(
    IOptions<ClientAppSettings> options,
    INotificationSender notificationSender,
    UserManager<User> userManager) : ICommandHandler<RequestPasswordResetCommand, RequestPasswordResetResult>
{
    private readonly ClientAppSettings _clientAppSettings = options.Value;

    public async Task<RequestPasswordResetResult> HandleAsync(RequestPasswordResetCommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if (user is null) return RequestPasswordResetResult.Succeeded();

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var passwordResetLink =
            $"{_clientAppSettings.BaseUrl}{_clientAppSettings.Paths.PasswordReset}?token={WebUtility.UrlEncode(token)}&userId={user.Id}";
        await notificationSender.SendAsync(
            new YetAnotherECommerce.Shared.Contracts.Notifications.Identity.PasswordResetRequested(user.Email!,
                passwordResetLink));

        return RequestPasswordResetResult.Succeeded();
    }
}