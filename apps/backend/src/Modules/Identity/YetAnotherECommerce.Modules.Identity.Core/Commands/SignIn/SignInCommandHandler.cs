using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using IAuthManager = YetAnotherECommerce.Modules.Identity.Core.Services.IAuthManager;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;

public class SignInCommandHandler(IAuthManager authManager, UserManager<User> userManager) : ICommandHandler<SignInCommand, SignInResult>
{
    public async Task<SignInResult> HandleAsync(SignInCommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email);

        if (user is not null && userManager.SupportsUserLockout && await userManager.IsLockedOutAsync(user))
        {
            return SignInResult.LockedOut();
        }

        var isValid = user is not null && await userManager.CheckPasswordAsync(user, command.Password);
        if (!isValid)
        {
            if (user is not null && userManager.SupportsUserLockout)
            {
                await userManager.AccessFailedAsync(user);
            }

            return SignInResult.InvalidCredentials();
        }

        if (!await userManager.IsEmailConfirmedAsync(user))
        {
            return SignInResult.EmailNotConfirmed();
        }

        if (userManager.SupportsUserLockout && await userManager.IsLockedOutAsync(user))
        {
            await userManager.ResetAccessFailedCountAsync(user);
        }
        
        var userRole = (await userManager.GetRolesAsync(user)).SingleOrDefault();
        var token = authManager.GenerateJwtToken(user.Id, userRole);
        return SignInResult.Succeeded(token);
    }
}