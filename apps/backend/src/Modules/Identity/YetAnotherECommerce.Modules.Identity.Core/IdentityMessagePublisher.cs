using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Identity.Core;

public class IdentityMessagePublisher(ServiceBusMessagePublisher messagePublisher) : IIdentityMessagePublisher
{
    public Task PublishAsync(IMessage message) => messagePublisher.PublishAsync(message, "identity");
}