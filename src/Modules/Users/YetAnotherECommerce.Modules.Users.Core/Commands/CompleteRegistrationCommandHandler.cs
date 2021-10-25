using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Modules.Users.Messages.Events;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Users.Core.Commands
{
    public class CompleteRegistrationCommandHandler : ICommandHandler<CompleteRegistrationCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public CompleteRegistrationCommandHandler(IUserRepository userRepository, IEventDispatcher eventDispatcher)
        {
            _userRepository = userRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(CompleteRegistrationCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            
            user.UpdateFirstName(command.FirstName);
            user.UpdateLastName(command.LastName);
            user.UpdateAddress(command.Street, command.City, command.ZipCode, command.Country);

            await _userRepository.UpdateAsync(user);

            await _eventDispatcher.PublishAsync(
                new RegistrationCompleted(user.Id, user.FirstName, user.LastName, user.Email, user.Address.ToString()));
        }
    }
}