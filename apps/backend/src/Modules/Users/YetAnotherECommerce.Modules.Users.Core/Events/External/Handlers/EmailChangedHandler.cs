using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Users.Core.Events.External.Handlers;

public class EmailChangedHandler(IUserRepository userRepository) : IEventHandler<EmailChanged>
{
    public async Task HandleAsync(EmailChanged @event)
    {
        var user = await userRepository.GetByIdAsync(@event.UserId);
        user.UpdateEmail(@event.Email);

        await userRepository.UpdateAsync(user);
    }
}