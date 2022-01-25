using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Events;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp
{
    public class SignUpCommandHandler : ICommandHandler<SignUpCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public SignUpCommandHandler(IUserRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(SignUpCommand command)
        {
            if (await _repository.CheckIfEmailIsInUseAsync(command.Email))
                throw new EmailInUseException();

            var user = new User(command.Email, command.Password, command.Role);
            await _repository.AddAsync(user);

            await _messageBroker.PublishAsync(new UserRegistered(user.Id, user.Email));
        }
    }
}