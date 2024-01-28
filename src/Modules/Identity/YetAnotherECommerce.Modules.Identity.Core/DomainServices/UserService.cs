using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Exceptions;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Identity.Core.DomainServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
            => _userRepository = userRepository;

        public async Task<User> CreateUserAsync(string email, string password, string role)
        {
            if (!Email.HasValidFormat(email))
                throw new InvalidEmailFormatException();

            if (await _userRepository.CheckIfEmailIsInUseAsync(email))
                throw new EmailInUseException();

            var emailAddress = Email.Create(email);
            var encryptedPassword = Password.Create(password);

            return User.Create(emailAddress, encryptedPassword, role);
        }

        public async Task ChangePasswordAsync(Guid userId, string password)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                throw new UserNotExistException(userId);

            if (user.Password.IsValid(password))
                throw new ProvidedPasswordIsExactlyTheSameAsTheCurrentOne();

            user.ChangePassword(Password.Create(password));

            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangeEmailAsync(Guid userId, string email)
        {
            if (!Email.HasValidFormat(email))
                throw new InvalidEmailFormatException();

            if (await _userRepository.CheckIfEmailIsInUseAsync(email))
                throw new EmailInUseException();

            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                throw new UserNotExistException(userId);

            user.ChangeEmail(email);

            await _userRepository.UpdateAsync(user);
        }
    }
}