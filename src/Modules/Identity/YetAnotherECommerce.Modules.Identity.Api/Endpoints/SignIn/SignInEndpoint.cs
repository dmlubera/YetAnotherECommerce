using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Api.Endpoints.SignIn;

public class SignInEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<SignInRequest>
{
    public override void Configure()
    {
        Post("/sign-in");
        Group<IdentityModuleEndpointsGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(SignInRequest req, CancellationToken ct)
    {
        var command = new SignInCommand(req.Email, req.Password);
        var result = await commandDispatcher.DispatchAsync(command);

        await result.Match(
            onSuccess: value => SendOkAsync(value, ct),
            onError: error => SendResultAsync(TypedResults.BadRequest(error)));
    }
}