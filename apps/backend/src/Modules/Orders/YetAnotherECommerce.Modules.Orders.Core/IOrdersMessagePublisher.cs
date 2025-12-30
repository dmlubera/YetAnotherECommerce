using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Orders.Core;

public interface IOrdersMessagePublisher
{
    Task PublishAsync(IMessage message);
}