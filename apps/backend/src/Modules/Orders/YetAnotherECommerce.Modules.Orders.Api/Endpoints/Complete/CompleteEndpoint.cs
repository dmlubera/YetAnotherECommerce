using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Orders.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Orders.Api.Endpoints.Complete;

public class CompleteEndpoint(ICommandDispatcher commandDispatcher) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("orders/{orderId:guid}/complete");
        Roles("admin");
        Group<OrdersModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await commandDispatcher.DispatchAsync(new CompleteOrderCommand(Route<Guid>("orderId")));
        await SendOkAsync(ct);
    }
}