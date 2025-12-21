using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Core.Events.External.Handlers;

public class OrderRevokedHandler(IProductRepository productRepository) : IEventHandler<OrderRevoked>
{
    public async Task HandleAsync(OrderRevoked @event)
    {
        var products = await productRepository.GetByIdsAsync(@event.Products.Keys);
        foreach (var product in products)
        {
            @event.Products.TryGetValue(product.Id, out var orderedQuantity);
            product.UpdateQuantity(product.Quantity + orderedQuantity);
        }

        await productRepository.UpdateAsync(products);
    }
}