using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;

namespace YetAnotherECommerce.Modules.Orders.Api.Endpoints;

public class HomeEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
        Group<OrdersModuleEndpointsGroup>();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        return SendAsync("Orders Module API", cancellation: ct);
    }
}