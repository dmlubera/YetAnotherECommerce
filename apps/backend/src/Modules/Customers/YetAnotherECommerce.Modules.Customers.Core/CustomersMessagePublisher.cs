using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Customers.Core;

public class CustomersMessagePublisher(IMessagePublisher messagePublisher) : ICustomersMessagePublisher
{
    public Task PublishAsync(IMessage message) => messagePublisher.PublishAsync(message, "carts");
}