using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Api.Models.Requests;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangeEmail;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Api.Controllers
{
    [Route("identity-module/account")]
    internal class AccountController: ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public AccountController(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task<IActionResult> ChangeEmailAsync([FromBody] ChangeEmailRequest request)
        {
            await _commandDispatcher.DispatchAsync(new ChangeEmailCommand(request.Email));
            
            return Ok();
        }
    }
}