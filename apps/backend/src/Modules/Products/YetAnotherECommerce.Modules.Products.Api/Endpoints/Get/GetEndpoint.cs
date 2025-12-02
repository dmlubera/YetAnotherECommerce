using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Products.Core.Queries;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Products.Api.Endpoints.Get;

public class GetEndpoint(IQueryDispatcher queryDispatcher) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("products/{id:guid}");
        AllowAnonymous();
        Group<ProductsModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = new GetProductDetailsQuery(Route<Guid>("id"));
        await SendOkAsync(await queryDispatcher.DispatchAsync(query), ct);
    }
}

