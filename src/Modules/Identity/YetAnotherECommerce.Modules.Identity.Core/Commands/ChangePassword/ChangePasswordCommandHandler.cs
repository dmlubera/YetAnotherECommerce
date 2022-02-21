using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.DomainServices;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IUserService _userService;

        public ChangePasswordCommandHandler(IUserService userService)
            => _userService = userService;

        public async Task HandleAsync(ChangePasswordCommand command)
            => await _userService.ChangePasswordAsync(command.UserId, command.Password);
    }
}