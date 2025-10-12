using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public class AsyncMessageDispatcher : IAsyncMessageDispatcher
    {
        private readonly IMessageChannel _messageChannel;

        public AsyncMessageDispatcher(IMessageChannel messageChannel)
            => _messageChannel = messageChannel;

        public async Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessageEnvelope
            => await _messageChannel.Writer.WriteAsync(message);
    }
}