using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Abstractions.Messages;

public interface IMessagePublisher
{
    Task PublishAsync(IMessage message, string source);
}