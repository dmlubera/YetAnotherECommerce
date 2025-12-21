using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages;

public class AsyncMessageDispatcher(IMessageChannel messageChannel) : IAsyncMessageDispatcher
{
    public async Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessageEnvelope
        => await messageChannel.Writer.WriteAsync(message);
}