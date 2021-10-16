using System;
using YetAnotherECommerce.Modules.Carts.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Cache;

namespace YetAnotherECommerce.Modules.Carts.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ICache _cache;

        public CartService(ICache cache)
            => _cache = cache;

        public Cart Browse(string cacheKey)
            => _cache.Get<Cart>(cacheKey);

        public void ClearCart(string cacheKey)
            => _cache.Clear(cacheKey);

        public void RemoveItem(string cacheKey, Guid itemId)
        {
            var cart =_cache.Get<Cart>(cacheKey);
            cart.RemoveItem(itemId);
        }
    }
}