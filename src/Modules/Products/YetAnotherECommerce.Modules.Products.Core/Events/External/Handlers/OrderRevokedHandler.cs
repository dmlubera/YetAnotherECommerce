using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Core.Events.External.Handlers
{
    public class OrderRevokedHandler : IEventHandler<OrderRevoked>
    {
        private readonly IProductRepository _productRepository;

        public OrderRevokedHandler(IProductRepository productRepository)
            => _productRepository = productRepository;

        public async Task HandleAsync(OrderRevoked @event)
        {
            // TODO: Figure out how to perform updates like this in more efficient way
            foreach (var orderedProduct in @event.Products)
            {
                var product = await _productRepository.GetByIdAsync(orderedProduct.Key);
                if (product is null) continue;

                product.UpdateQuantity(product.Quantity + orderedProduct.Value);

                await _productRepository.UpdateAsync(product);
            }
        }
    }
}