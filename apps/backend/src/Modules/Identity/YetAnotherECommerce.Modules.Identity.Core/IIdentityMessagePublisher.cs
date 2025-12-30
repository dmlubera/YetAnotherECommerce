using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Identity.Core;

public interface IIdentityMessagePublisher
{
    Task PublishAsync(IMessage message);
}