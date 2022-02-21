using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Users.Core.Events.External.Handlers
{
    public class EmailChangedHandler : IEventHandler<EmailChanged>
    {
        private readonly IUserRepository _userRepository;

        public EmailChangedHandler(IUserRepository userRepository)
            => _userRepository = userRepository;

        public async Task HandleAsync(EmailChanged @event)
        {
            var user = await _userRepository.GetByIdAsync(@event.UserId);
            user.UpdateEmail(@event.Email);

            await _userRepository.UpdateAsync(user);
        }
    }
}