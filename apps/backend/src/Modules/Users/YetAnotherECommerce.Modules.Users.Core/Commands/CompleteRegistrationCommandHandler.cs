using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.Events;
using YetAnotherECommerce.Modules.Users.Core.Exceptions;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Users.Core.Commands;

public class CompleteRegistrationCommandHandler(IUserRepository userRepository, IMessageBroker messageBroker)
    : ICommandHandler<CompleteRegistrationCommand>
{
    public async Task HandleAsync(CompleteRegistrationCommand command)
    {
        var user = await userRepository.GetByIdAsync(command.UserId);

        if (user.IsRegistrationCompleted)
            throw new RegistrationAlreadyCompletedException();

        user.UpdateFirstName(command.FirstName);
        user.UpdateLastName(command.LastName);
        user.UpdateAddress(command.Street, command.City, command.ZipCode, command.Country);
        user.CompleteRegistration();

        await userRepository.UpdateAsync(user);

        await messageBroker.PublishAsync(
            new RegistrationCompleted(user.Id, user.FirstName, user.LastName, user.Email, user.Address.ToString()));
    }
}