using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Api.Models.Requests;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Api.Controllers
{
    [Route("identity-module/")]
    internal class AuthController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public AuthController(ICommandDispatcher dispatcher)
            => _commandDispatcher = dispatcher;

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var command = new SignUpCommand(request.Email, request.Password);
            await _commandDispatcher.DispatchAsync(command);

            return Ok();
        }
    }
}