using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Api.Endpoints.AddToCart;

public class AddToCartEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<AddProductToCartRequest>
{
    public override void Configure()
    {
        Post("products/add-to-cart");
        Roles("customer");
        Group<ProductsModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(AddProductToCartRequest req, CancellationToken ct)
    {
        var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
        var command = new AddProductToCartCommand(userId, req.ProductId, req.Quantity);
        await commandDispatcher.DispatchAsync(command);

        await SendOkAsync(ct);
    }
}