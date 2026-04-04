using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ConfirmEmail;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Api.Endpoints.ConfirmEmail;

public class ConfirmEmailEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<ConfirmEmailRequest>
{
    public override void Configure()
    {
        Post("/confirm-email");
        Group<IdentityModuleEndpointsGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(ConfirmEmailRequest req, CancellationToken ct)
    {
        var command = new ConfirmEmailCommand(req.UserId, WebUtility.UrlDecode(req.Token));
        var result = await commandDispatcher.DispatchAsync(command);

        await result.Match(
            onSuccess: () => SendOkAsync(ct),
            onError: error => SendResultAsync(TypedResults.BadRequest(error)));
    }
    
}