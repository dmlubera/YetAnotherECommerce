using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Orders.Core.Queries;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Api.Endpoints.BrowseMyOrders;

public class BrowseMyOrdersEndpoint(IQueryDispatcher queryDispatcher) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("orders/my-orders");
        Roles("customer");
        Group<OrdersModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var customerId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
        var orders = await queryDispatcher.DispatchAsync(new BrowseCustomerOrdersQuery(customerId));
        await SendOkAsync(orders, ct);
    }
}