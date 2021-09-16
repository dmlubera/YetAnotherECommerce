using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Modules.Identity.Messages.Events;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp
{
    public class SignUpCommandHandler : ICommandHandler<SignUpCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IEventDispatcher _eventDispatcher;

        public SignUpCommandHandler(IUserRepository repository, IEventDispatcher eventDispatcher)
        {
            _repository = repository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(SignUpCommand command)
        {
            if (await _repository.CheckIfEmailIsInUseAsync(command.Email))
                throw new EmailInUseException();

            var user = new User(command.Email, command.Password, command.Password);
            await _repository.AddAsync(user);

            await _eventDispatcher.PublishAsync(new UserRegistered(user.Id, user.Email.Value, user.Password.Hash));
        }
    }
}