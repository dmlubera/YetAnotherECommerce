using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Events;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ChangeEmail
{
    public class ChangeEmailCommandHandler : ICommandHandler<ChangeEmailCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageBroker _messageBroker;

        public ChangeEmailCommandHandler(IUserRepository userRepository, IMessageBroker messageBroker)
        {
            _userRepository = userRepository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(ChangeEmailCommand command)
        {
            if (!Email.HasValidFormat(command.Email))
                throw new InvalidEmailFormatException();

            if (await _userRepository.CheckIfEmailIsInUseAsync(command.Email))
                throw new EmailInUseException();

            var user = await _userRepository.GetByIdAsync(command.UserId);

            if (user is null)
                throw new UserNotExistException(command.UserId);

            user.ChangeEmail(command.Email);

            await _userRepository.UpdateAsync(user);

            await _messageBroker.PublishAsync(new EmailChanged(user.Id, user.Email));
        }
    }
}