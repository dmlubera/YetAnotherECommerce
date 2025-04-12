using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Api.Models.Requests;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Api.Controllers;

[Route("identity-module/account")]
internal class AccountController(ICommandDispatcher commandDispatcher) : ControllerBase
{
    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        await commandDispatcher.DispatchAsync(new ChangePasswordCommand(Guid.Parse(User.Identity.Name), request.CurrentPassword, request.NewPassword));
        return Ok();
    }
}