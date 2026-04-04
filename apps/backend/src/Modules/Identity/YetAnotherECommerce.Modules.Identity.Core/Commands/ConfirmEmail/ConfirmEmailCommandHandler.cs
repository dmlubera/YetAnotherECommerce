using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler(UserManager<User> userManager) : ICommandHandler<ConfirmEmailCommand,  ConfirmEmailResult>
{
    public async Task<ConfirmEmailResult> HandleAsync(ConfirmEmailCommand command)
    {
        var user = await userManager.FindByIdAsync(command.UserId.ToString());
        
        if (user is null) return ConfirmEmailResult.UserNotFound();

        if (user.EmailConfirmed) return ConfirmEmailResult.Succeeded();

        var result = await userManager.ConfirmEmailAsync(user, command.Token);
        
        return result.Succeeded ? ConfirmEmailResult.Succeeded() : ConfirmEmailResult.InvalidToken();
    }
}