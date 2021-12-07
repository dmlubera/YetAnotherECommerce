using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.Entities;
using YetAnotherECommerce.Modules.Users.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Users.Core.Events.External.Handlers
{

    public class UserRegisteredHandler : IEventHandler<UserRegistered>
    {
        private readonly IUserRepository _userRepository;

        public UserRegisteredHandler(IUserRepository userRepository)
            => _userRepository = userRepository;

        public async Task HandleAsync(UserRegistered @event)
        {
            var user = new User(@event.Id, @event.Email, @event.Password);

            await _userRepository.AddAsync(user);
        }
    }
}