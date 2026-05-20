using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Catalog.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Catalog.Api.Endpoints.UpdateQuantity;

public class UpdateQuantityEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<UpdateQuantityRequest>
{
    public override void Configure()
    {
        Post("products/update-quantity");
        Roles("admin");
        Group<CatalogModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(UpdateQuantityRequest req, CancellationToken ct)
    {
        var command = new UpdateQuantityCommand(req.ProductId, req.Quantity);
        await commandDispatcher.DispatchAsync(command);
        await SendOkAsync(ct);
    }
}