using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Orders.Core.Queries;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Api.Endpoints.GetOrderDetails;

public class GetOrderDetailsEndpoint(IQueryDispatcher queryDispatcher) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("orders/my-orders/{orderId:guid}");
        Roles("customer");
        Group<OrdersModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var customerId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
        var orderDetails = await queryDispatcher.DispatchAsync(new GetOrderDetailsQuery(customerId, Route<Guid>("orderId")));
        await SendOkAsync(orderDetails, ct);
    }
}