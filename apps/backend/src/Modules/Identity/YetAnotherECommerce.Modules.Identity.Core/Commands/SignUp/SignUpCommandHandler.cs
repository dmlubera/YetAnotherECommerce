using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;

public class SignUpCommandHandler(UserManager<User> userManager)
    : ICommandHandler<SignUpCommand, SignUpResult>
{
    public async Task<SignUpResult> HandleAsync(SignUpCommand command)
    {
        var existingUser = await userManager.FindByEmailAsync(command.Email);
        if (existingUser is not null)
        {
            return SignUpResult.Failed();
        }
        
        var user = new User
        {
            Email = command.Email,
            UserName = command.Email
        };
        var result = await userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded)
        {
            return SignUpResult.Failed();
        }

        await userManager.AddToRoleAsync(user, "customer");
        user.MarkAsRegistered();

        return SignUpResult.Succeeded();
    }
}