using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}