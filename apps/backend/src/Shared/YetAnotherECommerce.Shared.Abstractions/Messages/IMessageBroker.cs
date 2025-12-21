using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Abstractions.Messages;

public interface IMessageBroker
{
    Task PublishAsync(IMessage message);
}