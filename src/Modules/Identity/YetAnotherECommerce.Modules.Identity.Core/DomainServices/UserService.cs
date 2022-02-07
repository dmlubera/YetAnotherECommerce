using System;
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
            var encryptedPassword = EncryptPassword(password);

            return User.Create(emailAddress, encryptedPassword, role);
        }

        public async Task ChangePasswordAsync(Guid userId, string password)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                throw new UserNotExistException(userId);

            if (!Password.HasValidFormat(password))
                throw new InvalidPasswordFormatException();

            if (_encrypter.IsEqual(user.Password.Hash, user.Password.Salt, password))
                throw new ProvidedPasswordIsExactlyTheSameAsTheCurrentOne();

            var encryptedPassword = EncryptPassword(password);
            user.ChangePassword(encryptedPassword);

            await _userRepository.UpdateAsync(user);
        }

        private Password EncryptPassword(string password)
        {
            var passwordSalt = _encrypter.GetSalt();
            var passwordHash = _encrypter.GetHash(password, passwordSalt);

            return Password.Create(passwordHash, passwordSalt);
        }
    }
}