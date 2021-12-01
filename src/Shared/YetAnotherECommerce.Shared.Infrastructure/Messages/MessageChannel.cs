using System.Threading.Channels;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public class MessageChannel : IMessageChannel
    {
        private readonly Channel<IMessage> _messages = Channel.CreateUnbounded<IMessage>();

        public ChannelReader<IMessage> Reader => _messages.Reader;

        public ChannelWriter<IMessage> Writer => _messages.Writer;
    }
}