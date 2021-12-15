using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Carts.Core.Entities;
using YetAnotherECommerce.Modules.Carts.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Carts.Core.Events.External.Handlers
{
    public class ProductAddedToCartHandler : IEventHandler<ProductAddedToCart>
    {
        private readonly ICache _cache;

        public ProductAddedToCartHandler(ICache cache)
        {
            _cache = cache;
        }

        public async Task HandleAsync(ProductAddedToCart @event)
        {
            var cacheKey = $"{@event.CustomerId}-cart";
            var cart = _cache.Get<Cart>(cacheKey);
            if (cart is null)
                cart = new Cart();

            var cartItem = new CartItem(@event.ProductId, @event.Name, @event.Quantity, @event.UnitPrice);
            cart.AddItem(cartItem);
            _cache.Set(cacheKey, cart);
            await Task.CompletedTask;
        }
    }
}