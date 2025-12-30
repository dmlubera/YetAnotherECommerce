using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Carts.Core;

public interface ICartsMessagePublisher
{
    Task PublishAsync(IMessage message);
}