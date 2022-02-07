using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Helpers;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Auth;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encypter;
        private readonly IAuthManager _authManager;
        private readonly ICache _cache;

        public SignInCommandHandler(IUserRepository userRepository, IEncrypter encrypter, IAuthManager authManager, ICache cache)
        {
            _userRepository = userRepository;
            _encypter = encrypter;
            _authManager = authManager;
            _cache = cache;
        }

        public async Task HandleAsync(SignInCommand command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);

            if (user is null || !_encypter.IsEqual(user.Password.Hash, user.Password.Salt, command.Password))
                throw new InvalidCredentialsException();

            var jwtToken = _authManager.GenerateJwtToken(user.Id, user.Role);
            _cache.Set(command.CacheKey, jwtToken);
        }
    }
}