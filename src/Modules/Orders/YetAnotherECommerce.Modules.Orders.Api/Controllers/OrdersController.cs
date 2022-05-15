using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Commands;
using YetAnotherECommerce.Modules.Orders.Core.Queries;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Api.Controllers
{
    [Route("orders-module/orders")]
    internal class OrdersController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public OrdersController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
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

        [HttpGet("my-orders/{orderId:guid}")]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> GetOrderDetailsAsync(Guid orderId)
        {
            var customerId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
            var orderDetails = await _queryDispatcher.DispatchAsync(new GetOrderDetailsQuery(customerId, orderId));

            return Ok(orderDetails);
        }

        [HttpPost("{orderId:guid}/cancel")]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> CancelOrderAsync(Guid orderId)
        {
            var customerId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
            await _commandDispatcher.DispatchAsync(new CancelOrderCommand(customerId, orderId));

            return Ok();
        }

        [HttpPost("{orderId:guid}/complete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CompleteOrderAsync(Guid orderId)
        {
            await _commandDispatcher.DispatchAsync(new CompleteOrderCommand(orderId));

            return Ok();
        }

        [HttpPost("{orderId:guid}/revoke")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RevokeOrderAsync(Guid orderId)
        {
            await _commandDispatcher.DispatchAsync(new RevokeOrderCommand(orderId));

            return Ok();
        }
    }
}