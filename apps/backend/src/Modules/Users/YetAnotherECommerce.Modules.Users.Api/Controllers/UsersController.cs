using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Api.Models.Requests;
using YetAnotherECommerce.Modules.Users.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Users.Api.Controllers
{
    [Route("users-module/users")]
    internal class UsersController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public UsersController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> CompleteRegistrationAsync([FromBody] CompleteRegistrationRequest request)
        {
            Guid userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
            await _commandDispatcher.DispatchAsync(
                new CompleteRegistrationCommand(userId, request.FirstName, request.LastName, request.Street,
                    request.City, request.ZipCode, request.Country));

            return Ok();
        }
    }
}