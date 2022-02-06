using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Helpers;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Identity.Core.DomainServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public async Task<User> CreateUserAsync(string email, string password, string role)
        {
            if (!Email.HasValidFormat(email))
                throw new InvalidEmailFormatException();

            if (!Password.HasValidFormat(password))
                throw new InvalidPasswordFormatException();

            if (await _userRepository.CheckIfEmailIsInUseAsync(email))
                throw new EmailInUseException();

            var emailAddress = Email.Create(email);
            var passwordSalt = _encrypter.GetSalt();
            var passwordHash = _encrypter.GetHash(password, passwordSalt);
            var encryptedPassword = Password.Create(passwordHash, passwordSalt);

            return User.Create(emailAddress, encryptedPassword, role);
        }
    }
}