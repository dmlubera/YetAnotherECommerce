using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using YetAnotherECommerce.Modules.Identity.Core.Commands.RequestPasswordReset;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Api.Endpoints.RequestPasswordReset;

public class RequestPasswordResetEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<RequestPasswordResetRequest>
{
    public override void Configure()
    {
        Post("/request-password-reset");
        Group<IdentityModuleEndpointsGroup>();
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(RequestPasswordResetRequest req, CancellationToken ct)
    {
        var command = new RequestPasswordResetCommand(req.Email);
        var result = await commandDispatcher.DispatchAsync(command);
        
        await result.Match(
            onSuccess: () => SendOkAsync(ct),
            onError: error => SendResultAsync(TypedResults.BadRequest(error)));
    }
}