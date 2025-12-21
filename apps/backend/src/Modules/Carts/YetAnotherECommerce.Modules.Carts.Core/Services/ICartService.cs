using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Carts.Core.Entities;

namespace YetAnotherECommerce.Modules.Carts.Core.Services;

public interface ICartService
{
    Cart Browse(string cacheKey);
    Task PlaceOrderAsync(Guid userId);
    void ClearCart(string cacheKey);
    void RemoveItem(string cacheKey, Guid itemId);
}