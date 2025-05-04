using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Api.Endpoints.ChangePassword;

public class ChangePasswordEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<ChangePasswordRequest>
{
    public override void Configure()
    {
        Post("/account/change-password");
        Group<IdentityModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(ChangePasswordRequest req, CancellationToken ct)
    {
        var command = new ChangePasswordCommand(Guid.Parse(User.Identity.Name), req.CurrentPassword, req.NewPassword);
        var result = await commandDispatcher.DispatchAsync(command);
        await result.Match(onSuccess: () => SendOkAsync(ct), onError:  error => SendResultAsync(Results.BadRequest(error)));
    }
}