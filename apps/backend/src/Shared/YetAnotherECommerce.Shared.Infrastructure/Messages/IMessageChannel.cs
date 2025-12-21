using System.Threading.Channels;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages;

public interface IMessageChannel
{
    public ChannelReader<IMessageEnvelope> Reader { get; }
    public ChannelWriter<IMessageEnvelope> Writer { get; }
}