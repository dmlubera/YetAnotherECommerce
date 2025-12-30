using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Identity.Core;

public class IdentityMessagePublisher(IMessagePublisher messagePublisher) : IIdentityMessagePublisher
{
    public Task PublishAsync(IMessage message) => messagePublisher.PublishAsync(message, "identity");
}