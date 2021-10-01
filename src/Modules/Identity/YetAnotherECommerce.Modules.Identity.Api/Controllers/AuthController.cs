using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Api.Models.Requests;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;
using YetAnotherECommerce.Shared.Abstractions.Auth;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Api.Controllers
{
    [Route("identity-module/")]
    internal class AuthController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ICache _cache;

        public AuthController(ICommandDispatcher dispatcher, ICache cache)
        {
            _commandDispatcher = dispatcher;
            _cache = cache;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var command = new SignUpCommand(request.Email, request.Password);
            await _commandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var command = new SignInCommand(request.Email, request.Password);
            await _commandDispatcher.DispatchAsync(command);

            return Ok(_cache.Get<JsonWebToken>(command.CacheKey));
        }
    }
}