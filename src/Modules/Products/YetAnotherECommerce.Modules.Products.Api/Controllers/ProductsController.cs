using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetProducts()
            => Ok(await _queryDispatcher.DispatchAsync(new GetAllProductsQuery()));


        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] AddProductRequest request)
        {
            var command = new AddProductCommand(request.Name, request.Description, request.Price);
            await _commandDispatcher.DispatchAsync(command);

            return Ok();
        }
    }
}