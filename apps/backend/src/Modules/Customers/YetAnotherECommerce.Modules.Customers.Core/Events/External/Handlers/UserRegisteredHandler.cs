using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Customers.Core.Entities;
using YetAnotherECommerce.Modules.Customers.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Customers.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Customers.Core.Events.External.Handlers;

public class UserRegisteredHandler(IUserRepository userRepository) : IEventHandler<UserRegistered>
{
    public async Task HandleAsync(UserRegistered @event)
    {
        var user = new User(@event.Id, @event.Email);

        await userRepository.AddAsync(user);
    }
}