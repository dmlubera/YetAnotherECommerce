using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YetAnotherECommerce.Modules.Products.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Core.Events.External.Handlers;

public class OrderCreatedHandler(
    IProductRepository productRepository,
    IProductsMessagePublisher messagePublisher,
    ILogger<OrderCreatedHandler> logger)
    : IEventHandler<OrderCreated>
{
    public async Task HandleAsync(OrderCreated @event)
    {
        try
        {
            var products = await productRepository.GetByIdsAsync(@event.Products.Keys);
            if (products.Count < @event.Products.Count)
                throw new SomeOfOrderedProductsAreNotAvailableException();
            foreach(var product in products)
            {
                @event.Products.TryGetValue(product.Id, out var orderedQuantity);
                if (product.Quantity < orderedQuantity)
                    throw new ProductIsNotAvailableInOrderedQuantityException();
                    
                product.UpdateQuantity(product.Quantity - orderedQuantity);
            }

            await productRepository.UpdateAsync(products);
            await messagePublisher.PublishAsync(new OrderAccepted(@event.OrderId));

            logger.LogInformation($"Order with ID: {@event.OrderId} has been accepted.");
        }
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);
            await messagePublisher.PublishAsync(new OrderRejected(@event.OrderId));
            logger.LogInformation($"Order with ID: {@event.OrderId} has been rejected.");

            throw;
        }
    }
}