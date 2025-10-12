using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public interface IMessageBroker
    {
        Task PublishAsync(IMessage message);
    }
}