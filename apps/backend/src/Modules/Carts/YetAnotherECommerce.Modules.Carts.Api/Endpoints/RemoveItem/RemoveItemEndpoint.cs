using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Carts.Core.Services;

namespace YetAnotherECommerce.Modules.Carts.Api.Endpoints.RemoveItem;

public class RemoveItemEndpoint(ICartService cartService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("carts/{itemId:guid}");
        Roles("customer");
        Group<CartsModuleEndpointsGroup>();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
        cartService.RemoveItem(userId, Route<Guid>("itemId"));
        await SendNoContentAsync(ct);
    }
}