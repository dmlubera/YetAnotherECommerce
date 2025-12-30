using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Users.Core;

public class UsersMessagePublisher(IMessagePublisher messagePublisher) : IUsersMessagePublisher
{
    public Task PublishAsync(IMessage message) => messagePublisher.PublishAsync(message, "carts");
}