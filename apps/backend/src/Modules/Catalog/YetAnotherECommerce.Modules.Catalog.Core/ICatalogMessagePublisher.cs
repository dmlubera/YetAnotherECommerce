using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Catalog.Core;

public interface ICatalogMessagePublisher
{
    Task PublishAsync(IMessage message);
}