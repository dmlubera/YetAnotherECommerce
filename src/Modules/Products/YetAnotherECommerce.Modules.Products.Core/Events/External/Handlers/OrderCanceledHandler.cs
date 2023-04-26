using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Products.Core.Events.External.Handlers
{
    [ServiceBusSubscription("productsmodule")]
    public class OrderCanceledHandler : IEventHandler<OrderCanceled>
    {
        private readonly IProductRepository _productRepository;

        public OrderCanceledHandler(IProductRepository productRepository)
            => _productRepository = productRepository;

        public async Task HandleAsync(OrderCanceled @event)
        {
            var products = await _productRepository.GetByIdsAsync(@event.Products.Keys);
            foreach(var product in products)
            {
                @event.Products.TryGetValue(product.Id, out var orderedQuantity);
                product.UpdateQuantity(product.Quantity + orderedQuantity);
            }

            await _productRepository.UpdateAsync(products);
        }
    }
}