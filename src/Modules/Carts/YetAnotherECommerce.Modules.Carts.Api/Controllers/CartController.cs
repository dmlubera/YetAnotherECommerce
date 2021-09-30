using Microsoft.AspNetCore.Mvc;
using YetAnotherECommerce.Modules.Carts.Core.Entities;
using YetAnotherECommerce.Shared.Abstractions.Cache;

namespace YetAnotherECommerce.Modules.Carts.Api.Controllers
{
    [Route("carts-module/carts")]
    internal class CartController : ControllerBase
    {
        private readonly ICache _cache;

        public CartController(ICache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IActionResult Get()
            => Ok(_cache.Get<Cart>("cart"));

        [HttpDelete]
        public IActionResult Clear()
        {
            _cache.Clear("cart");
            return NoContent();
        }
    }
}