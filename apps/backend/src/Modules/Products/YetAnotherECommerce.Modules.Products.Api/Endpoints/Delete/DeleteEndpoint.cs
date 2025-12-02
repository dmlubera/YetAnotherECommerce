using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Api.Endpoints.Delete;

public class DeleteEndpoint(ICommandDispatcher commandDispatcher) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("products/{id:guid}");
        Roles("admin");
        Group<ProductsModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var command = new DeleteProductCommand(Route<Guid>("id"));
        await commandDispatcher.DispatchAsync(command);
        await SendNoContentAsync(ct);
    }
}