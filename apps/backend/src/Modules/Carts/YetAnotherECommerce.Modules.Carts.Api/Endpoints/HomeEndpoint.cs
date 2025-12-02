using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;

namespace YetAnotherECommerce.Modules.Carts.Api.Endpoints;

public class HomeEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
        Group<CartsModuleEndpointsGroup>();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        return SendAsync("Carts Module API", cancellation: ct);
    }
}