using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ResendEmailConfirmation;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Api.Endpoints.ResendConfirmationEmail;

public class ResendEmailConfirmationEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<ResendEmailConfirmationRequest>
{
    public override void Configure()
    {
        Post("/resend-email-confirmation");
        Group<IdentityModuleEndpointsGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(ResendEmailConfirmationRequest req, CancellationToken ct)
    {
        var command = new ResendEmailConfirmationCommand(req.Email);
        var result = await commandDispatcher.DispatchAsync(command);

        await result.Match(
            onSuccess: () => SendOkAsync(ct),
            onError: error => SendResultAsync(TypedResults.BadRequest(error)));
    }
}