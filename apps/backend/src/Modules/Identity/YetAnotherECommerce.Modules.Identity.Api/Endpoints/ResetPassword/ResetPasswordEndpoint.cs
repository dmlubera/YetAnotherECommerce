using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ResetPassword;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Api.Endpoints.ResetPassword;

public class ResetPasswordEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<ResetPasswordRequest>
{
    public override void Configure()
    {
        Post("/reset-password");
        Group<IdentityModuleEndpointsGroup>();
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(ResetPasswordRequest req, CancellationToken ct)
    {
        var command = new ResetPasswordCommand(req.UserId, WebUtility.UrlDecode(req.Token), req.Password);
        var result = await commandDispatcher.DispatchAsync(command);
        
        await result.Match(
            onSuccess: () => SendOkAsync(ct),
            onError: error => SendResultAsync(TypedResults.BadRequest(error))); 
    }
}