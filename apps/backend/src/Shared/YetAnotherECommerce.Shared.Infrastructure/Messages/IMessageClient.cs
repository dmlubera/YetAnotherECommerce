using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public interface IMessageClient
    {
        Task PublishAsync(IMessageEnvelope message);
    }
}