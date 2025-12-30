using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Products.Core;

public class ProductsMessagePublisher(IMessagePublisher messagePublisher) : IProductsMessagePublisher
{
    public Task PublishAsync(IMessage message) => messagePublisher.PublishAsync(message, "products");
}