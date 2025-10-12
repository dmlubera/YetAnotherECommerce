using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword;

public class ChangePasswordCommandHandler(UserManager<User> userManager) : ICommandHandler<ChangePasswordCommand, ChangePasswordResult>
{
    public async Task<ChangePasswordResult> HandleAsync(ChangePasswordCommand command)
    {
        var user = await userManager.FindByIdAsync(command.UserId.ToString());
        if (user is null)
        {
            throw new UserNotExistException(command.UserId);
        }
        
        var result = await userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);
        return result.Succeeded ? ChangePasswordResult.Succeeded() : ChangePasswordResult.Failed();
    }
}