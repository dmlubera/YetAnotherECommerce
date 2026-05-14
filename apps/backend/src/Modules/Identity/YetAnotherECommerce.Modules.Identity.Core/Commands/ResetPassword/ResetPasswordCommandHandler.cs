using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ResetPassword;

public class ResetPasswordCommandHandler(UserManager<User> userManager)
    : ICommandHandler<ResetPasswordCommand, ResetPasswordResult>
{
    public async Task<ResetPasswordResult> HandleAsync(ResetPasswordCommand command)
    {
        var user = await userManager.FindByIdAsync(command.UserId.ToString());
        if (user is null) return ResetPasswordResult.UserNotFound();

        var result = await userManager.ResetPasswordAsync(user, command.Token, command.Password);
        if (!result.Succeeded) return ResetPasswordResult.Failed();

        user.EmailConfirmed = true;

        return ResetPasswordResult.Succeeded();
    }
}