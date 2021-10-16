using System;
using YetAnotherECommerce.Modules.Carts.Core.Entities;

namespace YetAnotherECommerce.Modules.Carts.Core.Services
{
    public interface ICartService
    {
        Cart Browse(string cacheKey);
        void ClearCart(string cacheKey);
        void RemoveItem(string cacheKey, Guid itemId);
    }
}