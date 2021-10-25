using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Carts.Core.Entities;
using YetAnotherECommerce.Modules.Carts.Core.Exceptions;
using YetAnotherECommerce.Modules.Carts.Messages.Events;
using YetAnotherECommerce.Modules.Carts.Messages.Model;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Carts.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ICache _cache;
        private readonly IEventDispatcher _eventDispatcher;
        public CartService(ICache cache, IEventDispatcher eventDispatcher)
        {
            _cache = cache;
            _eventDispatcher = eventDispatcher;
        }
        
        public Cart Browse(string cacheKey)
            => _cache.Get<Cart>(cacheKey);

        public async Task PlaceOrderAsync(Guid userId)
        {
            var cart = _cache.Get<Cart>($"{userId}-cart");

            if (cart.Items.Count == 0)
                throw new CannotCreateOrderFromEmptyCartException();

            var productDtos = new List<ProductDto>();
            foreach(var item in cart.Items)
            {
                if (item.Quantity == 0)
                    throw new CannotOrderProductInZeroQuantityException();
                productDtos.Add(new ProductDto(item.ProductId, item.Name, item.UnitPrice, item.Quantity));
            }

            await _eventDispatcher.PublishAsync(new OrderPlaced(userId, productDtos));
        }

        public void ClearCart(string cacheKey)
            => _cache.Clear(cacheKey);

        public void RemoveItem(string cacheKey, Guid itemId)
        {
            var cart =_cache.Get<Cart>(cacheKey);
            cart.RemoveItem(itemId);
        }
    }
}