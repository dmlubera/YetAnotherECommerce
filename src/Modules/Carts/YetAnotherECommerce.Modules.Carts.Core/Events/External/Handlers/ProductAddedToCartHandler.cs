﻿using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Carts.Core.Entities;
using YetAnotherECommerce.Modules.Products.Messages.Events;
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
            var cart = _cache.Get<Cart>("cart");
            if (cart is null)
                cart = new Cart();

            var cartItem = new CartItem(@event.ProductId, @event.Name, @event.Quantity, @event.UnitPrice);
            cart.AddItem(cartItem);
            await Task.CompletedTask;
            _cache.Set("cart", cart);
        }
    }
}