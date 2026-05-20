using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Customers.Core;

public interface ICustomersMessagePublisher
{
    Task PublishAsync(IMessage message);
}