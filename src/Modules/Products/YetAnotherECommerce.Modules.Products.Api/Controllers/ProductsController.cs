using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Api.Models.Requests;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.Queries;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Products.Api.Controllers
{
    [Route("products-module/products/")]
    public class ProductsController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ProductsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> BrowseAsync()
            => Ok(await _queryDispatcher.DispatchAsync(new BrowseProductsQuery()));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
            => Ok(await _queryDispatcher.DispatchAsync(new GetProductDetailsQuery(id)));


        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddAsync([FromBody] AddProductRequest request)
        {
            var command = new AddProductCommand(request.Name, request.Description, request.Price, request.Quantity);
            await _commandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _commandDispatcher.DispatchAsync(new DeleteProductCommand(id));
            return NoContent();
        }

        [HttpPost("update-quantity")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateQuantityAsync([FromBody] UpdateQuantityRequest request)
        {
            var command = new UpdateQuantityCommand(request.ProductId, request.Quantity);
            await _commandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpPost("add-to-cart")]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> AddToCartAsync([FromBody] AddProductToCartRequest request)
        {
            var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
            var command = new AddProductToCartCommand(userId, request.ProductId, request.Quantity);
            await _commandDispatcher.DispatchAsync(command);

            return Ok();
        }
    }
}