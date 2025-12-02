using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Orders.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Orders.Api.Endpoints.Revoke;

public class RevokeEndpoint(ICommandDispatcher commandDispatcher) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("orders/{orderId:guid}/revoke");
        Roles("admin");
        Group<OrdersModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await commandDispatcher.DispatchAsync(new RevokeOrderCommand(Route<Guid>("orderId")));
        await SendOkAsync(ct);
    }
}