using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.Entities;
using YetAnotherECommerce.Modules.Users.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Users.Core.Events.External.Handlers;

public class UserRegisteredHandler(IUserRepository userRepository) : IEventHandler<UserRegistered>
{
    public async Task HandleAsync(UserRegistered @event)
    {
        var user = new User(@event.Id, @event.Email);

        await userRepository.AddAsync(user);
    }
}