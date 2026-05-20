using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Catalog.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Catalog.Api.Endpoints.UpdatePrice;

public class UpdatePriceEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<UpdatePriceRequest>
{
    public override void Configure()
    {
        Post("products/update-price");
        Roles("admin");
        Group<CatalogModuleEndpointsGroup>();
    }
    
    public override async Task HandleAsync(UpdatePriceRequest req, CancellationToken ct)
    {
        var command = new UpdatePriceCommand(req.ProductId, req.Price);
        await commandDispatcher.DispatchAsync(command);
        await SendOkAsync(ct);
    }

}