using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Carts.Core.Services;

namespace YetAnotherECommerce.Modules.Carts.Api.Endpoints.ClearCart;

public class ClearCartEndpoint(ICartService cartService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("carts/");
        Roles("customer");
        Group<CartsModuleEndpointsGroup>();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
        cartService.ClearCart($"{userId}-cart");

        await SendNoContentAsync(ct);
    }
}