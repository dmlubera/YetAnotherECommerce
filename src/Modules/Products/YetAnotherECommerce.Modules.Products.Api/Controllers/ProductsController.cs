using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Api.Models.Requests;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Api.Controllers
{
    [Route("products-module/")]
    public class ProductsController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public ProductsController(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] AddProductRequest request)
        {
            var command = new AddProductCommand(request.Name, request.Description, request.Price);
            await _commandDispatcher.DispatchAsync(command);

            return Ok();
        }
    }
}