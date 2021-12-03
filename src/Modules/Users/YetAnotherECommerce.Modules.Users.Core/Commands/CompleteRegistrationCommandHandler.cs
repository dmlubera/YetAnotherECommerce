using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.Events;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Users.Core.Commands
{
    public class CompleteRegistrationCommandHandler : ICommandHandler<CompleteRegistrationCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageBroker _messageBroker;

        public CompleteRegistrationCommandHandler(IUserRepository userRepository, IMessageBroker messageBroker)
        {
            _userRepository = userRepository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(CompleteRegistrationCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            
            user.UpdateFirstName(command.FirstName);
            user.UpdateLastName(command.LastName);
            user.UpdateAddress(command.Street, command.City, command.ZipCode, command.Country);
            user.CompleteRegistration();

            await _userRepository.UpdateAsync(user);

            await _messageBroker.PublishAsync(
                new RegistrationCompleted(user.Id, user.FirstName, user.LastName, user.Email, user.Address.ToString()));
        }
    }
}