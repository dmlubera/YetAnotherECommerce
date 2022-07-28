using System.Threading.Channels;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public class MessageChannel : IMessageChannel
    {
        private readonly Channel<IMessageEnvelope> _messages = Channel.CreateUnbounded<IMessageEnvelope>();

        public ChannelReader<IMessageEnvelope> Reader => _messages.Reader;

        public ChannelWriter<IMessageEnvelope> Writer => _messages.Writer;
    }
}