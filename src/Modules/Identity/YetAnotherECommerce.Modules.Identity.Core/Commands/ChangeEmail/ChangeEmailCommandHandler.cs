using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
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

        public Task HandleAsync(ChangeEmailCommand command)
        {
            throw new NotImplementedException();
        }
    }
}