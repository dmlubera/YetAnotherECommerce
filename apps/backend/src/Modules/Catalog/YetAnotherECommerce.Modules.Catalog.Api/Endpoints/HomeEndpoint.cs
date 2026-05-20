using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;

namespace YetAnotherECommerce.Modules.Catalog.Api.Endpoints;

public class HomeEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
        Group<CatalogModuleEndpointsGroup>();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        return SendAsync("Catalog Module API", cancellation: ct);
    }
}