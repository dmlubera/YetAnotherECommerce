using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Api.Endpoints.SignUp;

public class SignUpEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<SignUpRequest>
{
    public override void Configure()
    {
        Post("/sign-up");
        Group<IdentityModuleEndpointsGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(SignUpRequest req, CancellationToken ct)
    {
        var command = new SignUpCommand(req.Email, req.Password, req.Role);
        await commandDispatcher.DispatchAsync(command);
        await SendOkAsync(ct);
    }
}