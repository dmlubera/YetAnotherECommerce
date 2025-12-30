using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.Events;
using YetAnotherECommerce.Modules.Users.Core.Exceptions;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Users.Core.Commands;

public class CompleteRegistrationCommandHandler(IUserRepository userRepository, IUsersMessagePublisher messagePublisher)
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

        await messagePublisher.PublishAsync(
            new RegistrationCompleted(user.Id, user.FirstName, user.LastName, user.Email, user.Address.ToString()));
    }
}