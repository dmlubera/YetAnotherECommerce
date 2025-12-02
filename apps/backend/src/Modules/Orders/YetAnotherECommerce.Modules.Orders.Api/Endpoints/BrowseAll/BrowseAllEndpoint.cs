using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Orders.Core.Queries;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Orders.Api.Endpoints.BrowseAll;

public class BrowseAllEndpoint(IQueryDispatcher queryDispatcher) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("orders/");
        Roles("admin");
        Group<OrdersModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var orders = await queryDispatcher.DispatchAsync(new BrowseQuery());
        await SendOkAsync(orders, ct);
    }
}