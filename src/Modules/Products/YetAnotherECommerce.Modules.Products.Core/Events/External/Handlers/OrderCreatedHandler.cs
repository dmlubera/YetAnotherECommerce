using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Products.Core.Events.External.Handlers
{
    public class OrderCreatedHandler : IEventHandler<OrderCreated>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMessageBroker _messageBroker;

        public OrderCreatedHandler(IProductRepository productRepository, IMessageBroker messageBroker)
        {
            _productRepository = productRepository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(OrderCreated @event)
        {
            try
            {
                var products = await _productRepository.GetByIdsAsync(@event.Products.Keys);
                if (products.Count < @event.Products.Count)
                    throw new SomeOfOrderedProductsAreNotAvailableException();
                foreach(var product in products)
                {
                    @event.Products.TryGetValue(product.Id, out var orderedQuantity);
                    if (product.Quantity < orderedQuantity)
                        throw new ProductIsNotAvailableInOrderedQuantityException();
                    
                    product.UpdateQuantity(product.Quantity - orderedQuantity);
                }

                await _productRepository.UpdateAsync(products);
                await _messageBroker.PublishAsync(new OrderAccepted(@event.OrderId));
            }
            catch(Exception ex)
            {
                await _messageBroker.PublishAsync(new OrderRejected(@event.OrderId));
                throw;
            }
        }
    }
}