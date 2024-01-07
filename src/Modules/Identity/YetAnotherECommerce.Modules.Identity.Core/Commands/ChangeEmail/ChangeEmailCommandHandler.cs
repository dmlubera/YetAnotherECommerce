using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.DomainServices;
using YetAnotherECommerce.Modules.Identity.Core.Events;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ChangeEmail
{
    public class ChangeEmailCommandHandler : ICommandHandler<ChangeEmailCommand>
    {
        private readonly IUserService _userService;
        private readonly IMessageBroker _messageBroker;

        public ChangeEmailCommandHandler(IUserService userService,
            IMessageBroker messageBroker)
        {
            _userService = userService;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(ChangeEmailCommand command)
        {
            await _userService.ChangeEmailAsync(command.UserId, command.Email);

            await _messageBroker.PublishAsync(new EmailChanged(command.UserId, command.Email));
        }
    }
}