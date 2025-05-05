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
        if (user is null)
        {
            return SignInResult.InvalidCredentials();
        }
        
        var isCorrectPassword = await userManager.CheckPasswordAsync(user, command.Password);
        if (!isCorrectPassword)
        {
            return SignInResult.InvalidCredentials();
        }
        
        var userRole = (await userManager.GetRolesAsync(user)).SingleOrDefault();
        var token = authManager.GenerateJwtToken(user.Id, userRole);
        return SignInResult.Succeeded(token);
    }
}