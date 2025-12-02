using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Products.Core.Queries;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Products.Api.Endpoints.Browse;

public class BrowseEndpoint(IQueryDispatcher queryDispatcher) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("products/");
        AllowAnonymous();
        Group<ProductsModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(await queryDispatcher.DispatchAsync(new BrowseProductsQuery()), ct);
    }
}