using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Carts.Core.Services;

namespace YetAnotherECommerce.Modules.Carts.Api.Controllers
{
    [Route("carts-module/carts")]
    internal class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Authorize(Roles = "customer")]
        public IActionResult Get()
        {
            var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
            return Ok(_cartService.Browse($"{userId}-cart"));
        }

        [HttpPost]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
            await _cartService.PlaceOrderAsync(userId);
            return Ok();
        }

        [HttpDelete("{itemId:guid}")]
        [Authorize(Roles = "customer")]
        public IActionResult RemoveItemAsync(Guid itemId)
        {
            var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
            _cartService.RemoveItem($"{userId}-cart", itemId);
            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "customer")]
        public IActionResult Clear()
        {
            var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
            _cartService.ClearCart($"{userId}-cart");
            return NoContent();
        }
    }
}