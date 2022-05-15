using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.DomainServices;
using YetAnotherECommerce.Modules.Identity.Core.Events;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp
{
    public class SignUpCommandHandler : ICommandHandler<SignUpCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _userService;
        private readonly IMessageBroker _messageBroker;

        public SignUpCommandHandler(IUserRepository repository, IUserService userService, IMessageBroker messageBroker)
        {
            _repository = repository;
            _userService = userService;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(SignUpCommand command)
        {
            var user = await _userService.CreateUserAsync(command.Email, command.Password, command.Role);

            await _repository.AddAsync(user);

            await _messageBroker.PublishAsync(new UserRegistered(user.Id, user.Email));
        }
    }
}