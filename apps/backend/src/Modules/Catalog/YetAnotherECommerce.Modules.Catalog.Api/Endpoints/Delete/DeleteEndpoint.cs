using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Catalog.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Catalog.Api.Endpoints.Delete;

public class DeleteEndpoint(ICommandDispatcher commandDispatcher) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("products/{id:guid}");
        Roles("admin");
        Group<CatalogModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var command = new DeleteProductCommand(Route<Guid>("id"));
        await commandDispatcher.DispatchAsync(command);
        await SendNoContentAsync(ct);
    }
}