using System.Threading.Channels;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public interface IMessageChannel
    {
        public ChannelReader<IMessage> Reader { get; }
        public ChannelWriter<IMessage> Writer { get; }
    }
}