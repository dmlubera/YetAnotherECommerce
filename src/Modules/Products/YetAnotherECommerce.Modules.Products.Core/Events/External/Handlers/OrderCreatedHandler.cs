using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Messages.Events;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Modules.Products.Messages.Events;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Core.Events.External.Handlers
{
    public class OrderCreatedHandler : IEventHandler<OrderCreated>
    {
        private readonly IProductRepository _productRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public OrderCreatedHandler(IProductRepository productRepository, IEventDispatcher eventDispatcher)
        {
            _productRepository = productRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(OrderCreated @event)
        {
            foreach(var orderedProduct in @event.Products)
            {
                var product = await _productRepository.GetByIdAsync(orderedProduct.Key);
                if (product is null || product.Quantity < orderedProduct.Value)
                {
                    await _eventDispatcher.PublishAsync(new OrderRejected(@event.OrderId));
                }
                else
                {
                    product.UpdateQuantity(product.Quantity - orderedProduct.Value);
                    await _productRepository.UpdateAsync(product);
                }
            }

            await _eventDispatcher.PublishAsync(new OrderAccepted(@event.OrderId));
        }
    }
}