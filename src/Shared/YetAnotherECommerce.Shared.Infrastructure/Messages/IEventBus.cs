using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public interface IEventBus
    {
        Task PublishAsync(IEvent @event);
        Task SetupAsync();
    }
}