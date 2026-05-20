using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;

namespace YetAnotherECommerce.Modules.Customers.Api.Endpoints;

public class HomeEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
        Group<CustomersModuleEndpointsGroup>();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        return SendAsync("Customers Module API", cancellation: ct);
    }
}