using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;
using YetAnotherECommerce.Shared.Abstractions.Auth;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.Api.Endpoints.SignIn;

public class SignInEndpoint(ICommandDispatcher commandDispatcher, ICache cache) : Endpoint<SignInRequest>
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
        await commandDispatcher.DispatchAsync(command);
        await SendOkAsync(cache.Get<JsonWebToken>(command.CacheKey), ct);
    }
}