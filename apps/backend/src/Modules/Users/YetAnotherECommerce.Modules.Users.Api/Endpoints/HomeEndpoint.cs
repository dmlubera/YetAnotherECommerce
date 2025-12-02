using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;

namespace YetAnotherECommerce.Modules.Users.Api.Endpoints;

public class HomeEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
        Group<UsersModuleEndpointsGroup>();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        return SendAsync("Users Module API", cancellation: ct);
    }
}