using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Api.Endpoints.UpdateQuantity;

public class UpdateQuantityEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<UpdateQuantityRequest>
{
    public override void Configure()
    {
        Post("products/update-quantity");
        Roles("admin");
        Group<ProductsModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(UpdateQuantityRequest req, CancellationToken ct)
    {
        var command = new UpdateQuantityCommand(req.ProductId, req.Quantity);
        await commandDispatcher.DispatchAsync(command);
        await SendOkAsync(ct);
    }
}