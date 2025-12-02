using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Carts.Core.Services;

namespace YetAnotherECommerce.Modules.Carts.Api.Endpoints.PlaceOrder;

public class PlaceOrderEndpoint(ICartService cartService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("carts/");
        Roles("customer");
        Group<CartsModuleEndpointsGroup>();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
        await cartService.PlaceOrderAsync(userId);
        await SendOkAsync(ct);
    }
}