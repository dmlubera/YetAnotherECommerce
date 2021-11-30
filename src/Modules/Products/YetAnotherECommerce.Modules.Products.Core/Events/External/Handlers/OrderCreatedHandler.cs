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