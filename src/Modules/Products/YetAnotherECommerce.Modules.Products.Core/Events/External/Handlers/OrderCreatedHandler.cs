using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Messages.Events;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
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
            try
            {
                foreach(var orderedProduct in @event.Products)
                {
                    var product = await _productRepository.GetByIdAsync(orderedProduct.Key);
                    if (product is null)
                        throw new ProductDoesNotExistException(orderedProduct.Key);
                    if (product.Quantity < orderedProduct.Value)
                        throw new ProductIsNotAvailableInOrderedQuantityException();
                    
                    //TODO: Update can be performed only if all products exist and are avaiable
                    product.UpdateQuantity(product.Quantity - orderedProduct.Value);
                    await _productRepository.UpdateAsync(product);
                }

                await _eventDispatcher.PublishAsync(new OrderAccepted(@event.OrderId));
            }
            catch(Exception ex)
            {
                await _eventDispatcher.PublishAsync(new OrderRejected(@event.OrderId));
                throw;
            }
        }
    }
}