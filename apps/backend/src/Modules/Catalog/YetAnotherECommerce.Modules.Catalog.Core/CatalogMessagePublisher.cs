using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Catalog.Core;

public class CatalogMessagePublisher(IMessagePublisher messagePublisher) : ICatalogMessagePublisher
{
    public Task PublishAsync(IMessage message) => messagePublisher.PublishAsync(message, "products");
}