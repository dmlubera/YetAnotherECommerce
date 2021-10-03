using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ChangeEmail
{
    public class ChangeEmailCommandHandler : ICommandHandler<ChangeEmailCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public ChangeEmailCommandHandler(IUserRepository userRepository, IEventDispatcher eventDispatcher)
        {
            _userRepository = userRepository;
            _eventDispatcher = eventDispatcher;
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

            if (user.Email == command.Email)
                throw new ProvidedEmailIsExactlyTheSameAsTheCurrentOneException();
        }
    }
}