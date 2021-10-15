using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [Authorize(Roles = "customer")]
        public IActionResult Get()
        {
            var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
            return Ok(_cache.Get<Cart>($"{userId}-cart"));
        }

        [HttpDelete]
        [Authorize(Roles = "customer")]
        public IActionResult Clear()
        {
            var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
            _cache.Clear($"{userId}-cart");
            return NoContent();
        }
    }
}