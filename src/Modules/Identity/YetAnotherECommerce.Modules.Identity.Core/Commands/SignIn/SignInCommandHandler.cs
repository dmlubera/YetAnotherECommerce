using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Auth;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;

public class SignInCommandHandler(IAuthManager authManager, UserManager<User> userManager) : ICommandHandler<SignInCommand, SignInResult>
{
    public async Task<SignInResult> HandleAsync(SignInCommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            return SignInResult.InvalidCredentials();
        }
        
        var isCorrectPassword = await userManager.CheckPasswordAsync(user, command.Password);
        return isCorrectPassword
            ? SignInResult.Succeeded(authManager.GenerateJwtToken(user.Id, user.Role))
            : SignInResult.InvalidCredentials();
    }
}