using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public interface IAsyncMessageDispatcher
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
    }
}