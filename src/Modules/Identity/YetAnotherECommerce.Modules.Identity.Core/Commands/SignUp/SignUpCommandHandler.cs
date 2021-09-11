using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp
{
    public class SignUpCommandHandler : ICommandHandler<SignUpCommand>
    {
        private readonly IUserRepository _repository;

        public SignUpCommandHandler(IUserRepository repository)
            => _repository = repository;

        public async Task HandleAsync(SignUpCommand command)
        {
            var user = await _repository.GetByEmailAsync(command.Email);

            if (user is null)
                await _repository.AddAsync(new User(command.Email, command.Password, command.Password));
        }
    }
}