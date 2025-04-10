using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Events;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;

public class SignUpCommandHandler(UserManager<User> userManager, IMessageBroker messageBroker) : ICommandHandler<SignUpCommand>
{
    public async Task HandleAsync(SignUpCommand command)
    {
        var existingUser = await userManager.FindByEmailAsync(command.Email);
        if (existingUser != null)
        {
            throw new EmailInUseException();
        }
        
        var user = new User
        {
            Email = command.Email,
            UserName = command.Email,
            Role = command.Role
        };
        var result = await userManager.CreateAsync(user, command.Password);
        if (result.Succeeded)
        {
            await messageBroker.PublishAsync(new UserRegistered(user.Id, user.Email));
        }
    }
}