using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Carts.Core.Entities;
using YetAnotherECommerce.Modules.Carts.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Carts.Core.Events.External.Handlers;

public class ProductAddedToCartHandler(ICache cache) : IEventHandler<ProductAddedToCart>
{
    public async Task HandleAsync(ProductAddedToCart @event)
    {
        var cacheKey = $"{@event.CustomerId}-cart";
        var cart = cache.Get<Cart>(cacheKey);
        if (cart is null)
            cart = new Cart();

        var cartItem = new CartItem(@event.ProductId, @event.Name, @event.Quantity, @event.UnitPrice);
        cart.AddItem(cartItem);
        cache.Set(cacheKey, cart);
        await Task.CompletedTask;
    }
}