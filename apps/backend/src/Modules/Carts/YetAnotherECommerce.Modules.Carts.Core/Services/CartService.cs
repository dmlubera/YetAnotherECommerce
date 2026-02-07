using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YetAnotherECommerce.Modules.Carts.Core.Cache;
using YetAnotherECommerce.Modules.Carts.Core.Entities;
using YetAnotherECommerce.Modules.Carts.Core.Events;
using YetAnotherECommerce.Modules.Carts.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.Cache;

namespace YetAnotherECommerce.Modules.Carts.Core.Services;

public class CartService(ICache cache, ICartsMessagePublisher messagePublisher, ILogger<CartService> logger)
    : ICartService
{
    public Cart Browse(Guid userId)
        => cache.Get(new CartCacheKey(userId));

    public async Task PlaceOrderAsync(Guid userId)
    {
        var cart = cache.Get(new CartCacheKey(userId));

        if (cart.Items.Count == 0)
            throw new CannotCreateOrderFromEmptyCartException();

        var productDtos = new List<ProductDto>();
        foreach(var item in cart.Items)
        {
            if (item.Quantity == 0)
                throw new CannotOrderProductInZeroQuantityException();
            productDtos.Add(new ProductDto(item.ProductId, item.Name, item.UnitPrice, item.Quantity));
        }

        var orderPlaced = new OrderPlaced(userId, productDtos);
        await messagePublisher.PublishAsync(orderPlaced);

        logger.LogInformation("Order placed: {@order}", orderPlaced);
    }

    public void ClearCart(Guid userId)
        => cache.Clear(new CartCacheKey(userId));

    public void RemoveItem(Guid userId, Guid itemId)
    {
        var cart =cache.Get(new CartCacheKey(userId));
        cart.RemoveItem(itemId);
    }
}