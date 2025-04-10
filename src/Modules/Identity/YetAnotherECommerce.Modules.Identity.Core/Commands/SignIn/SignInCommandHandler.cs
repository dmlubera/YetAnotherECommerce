using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.Auth;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;

public class SignInCommandHandler(IAuthManager authManager, ICache cache, UserManager<User> userManager) : ICommandHandler<SignInCommand>
{
    public async Task HandleAsync(SignInCommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email) ?? throw new InvalidCredentialsException();
        var isCorrectPassword = await userManager.CheckPasswordAsync(user, command.Password);
        if (!isCorrectPassword)
        {
            throw new InvalidCredentialsException();
        }
            
        var jwtToken = authManager.GenerateJwtToken(user.Id, user.Role);
        cache.Set(command.CacheKey, jwtToken);
    }
}