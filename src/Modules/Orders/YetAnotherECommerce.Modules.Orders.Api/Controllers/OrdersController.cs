using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Queries;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Api.Controllers
{
    [Route("orders-module/orders")]
    internal class OrdersController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public OrdersController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> BrowseAsync()
        {
            var orders = await _queryDispatcher.DispatchAsync(new BrowseQuery());

            return Ok(orders);
        }

        [HttpGet("my-orders")]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> BrowseByCustomerAsync()
        {
            var customerId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
            var orders = await _queryDispatcher.DispatchAsync(new BrowseCustomerOrdersQuery(customerId));

            return Ok(orders);
        }
    }
}