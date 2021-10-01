using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.Auth;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthManager _authManager;
        private readonly ICache _cache;

        public SignInCommandHandler(IUserRepository userRepository, IAuthManager authManager, ICache cache)
        {
            _userRepository = userRepository;
            _authManager = authManager;
            _cache = cache;
        }

        public async Task HandleAsync(SignInCommand command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);

            if (user is null)
                throw new UserNotExistException(command.Email);

            if (!Password.IsValid(user.Password, command.Password))
                throw new InvalidCredentialsException();

            var jwtToken = _authManager.GenerateJwtToken(user.Id);
            _cache.Set(command.CacheKey, jwtToken);
        }
    }
}