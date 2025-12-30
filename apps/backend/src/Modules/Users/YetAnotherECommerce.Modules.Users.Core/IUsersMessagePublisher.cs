using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Users.Core;

public interface IUsersMessagePublisher
{
    Task PublishAsync(IMessage message);
}