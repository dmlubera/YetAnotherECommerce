using System;
using YetAnotherECommerce.Modules.Carts.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Cache;

namespace YetAnotherECommerce.Modules.Carts.Core.Cache;

public class CartCacheKey(Guid userId) : ICacheKey<Cart>
{
    public string CacheKey => $"{userId}-cart";
}