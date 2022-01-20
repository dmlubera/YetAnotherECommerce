using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUserRepository userRepository)
            => _userRepository = userRepository;

        public async Task HandleAsync(ChangePasswordCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user is null)
                throw new UserNotExistException(command.UserId);

            if (Password.Match(user.Password, command.Password))
                throw new ProvidedPasswordIsExactlyTheSameAsTheCurrentOne();

            user.ChangePassword(command.Password);
            await _userRepository.UpdateAsync(user);
        }
    }
}