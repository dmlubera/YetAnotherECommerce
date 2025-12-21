using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Carts.Core.Entities;
using YetAnotherECommerce.Modules.Carts.Core.Events;
using YetAnotherECommerce.Modules.Carts.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Carts.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ICache _cache;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<CartService> _logger;

        public CartService(ICache cache, IMessageBroker messageBroker, ILogger<CartService> logger)
        {
            _cache = cache;
            _messageBroker = messageBroker;
            _logger = logger;
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

            var orderPlaced = new OrderPlaced(userId, productDtos);
            await _messageBroker.PublishAsync(orderPlaced);

            _logger.LogInformation("Order placed: {@order}", orderPlaced);
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