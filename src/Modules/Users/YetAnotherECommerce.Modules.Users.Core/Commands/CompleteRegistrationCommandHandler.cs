using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Users.Core.Commands
{
    public class CompleteRegistrationCommandHandler : ICommandHandler<CompleteRegistrationCommand>
    {
        private readonly IUserRepository _userRepository;

        public CompleteRegistrationCommandHandler(IUserRepository userRepository)
            => _userRepository = userRepository;

        public async Task HandleAsync(CompleteRegistrationCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            
            user.UpdateFirstName(command.FirstName);
            user.UpdateLastName(command.LastName);
            user.UpdateAddress(command.Street, command.City, command.ZipCode, command.Country);

            await _userRepository.UpdateAsync(user);
        }
    }
}