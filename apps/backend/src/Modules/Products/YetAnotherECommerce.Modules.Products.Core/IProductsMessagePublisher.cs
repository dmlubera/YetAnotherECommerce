using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Products.Core;

public interface IProductsMessagePublisher
{
    Task PublishAsync(IMessage message);
}