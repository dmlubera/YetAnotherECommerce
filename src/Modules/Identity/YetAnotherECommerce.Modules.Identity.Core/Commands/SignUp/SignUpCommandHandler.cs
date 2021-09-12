using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
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
            var existingUser = await _repository.GetByEmailAsync(command.Email);

            if (existingUser is null)
            {
                var user = new User(command.Email, command.Password, command.Password);
                await _repository.AddAsync(user);

                await _eventDispatcher.PublishAsync(new UserRegistered(user.Id, user.Email.Value, user.Password.Hash));
            }
        }
    }
}