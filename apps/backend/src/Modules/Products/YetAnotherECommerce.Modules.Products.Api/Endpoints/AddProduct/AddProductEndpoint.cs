using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Api.Endpoints.AddProduct;

public class AddProductEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<AddProductRequest>
{
    public override void Configure()
    {
        Post("products/");
        Roles("admin");
        Group<ProductsModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(AddProductRequest req, CancellationToken ct)
    {
        var command = new AddProductCommand(req.Name, req.Description, req.Price, req.Quantity);
        await commandDispatcher.DispatchAsync(command);
        await SendOkAsync(ct);
    }
}