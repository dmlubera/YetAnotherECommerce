using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Carts.Core;

public class CartsMessagePublisher(IMessagePublisher messagePublisher) : ICartsMessagePublisher
{
    public Task PublishAsync(IMessage message) => messagePublisher.PublishAsync(message, "carts");
}