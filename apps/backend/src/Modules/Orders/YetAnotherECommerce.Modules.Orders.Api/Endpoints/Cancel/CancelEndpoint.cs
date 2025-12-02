using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Orders.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Orders.Api.Endpoints.Cancel;

public class CancelEndpoint(ICommandDispatcher commandDispatcher) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("orders/my-orders/{orderId:guid}/cancel");
        Roles("customer");
        Group<OrdersModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var customerId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
        await commandDispatcher.DispatchAsync(new CancelOrderCommand(customerId, Route<Guid>("orderId")));
        await SendOkAsync(ct);
    }
}