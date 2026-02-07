using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Carts.Core.Entities;

namespace YetAnotherECommerce.Modules.Carts.Core.Services;

public interface ICartService
{
    Cart Browse(Guid userId);
    Task PlaceOrderAsync(Guid userId);
    void ClearCart(Guid userId);
    void RemoveItem(Guid userId, Guid itemId);
}