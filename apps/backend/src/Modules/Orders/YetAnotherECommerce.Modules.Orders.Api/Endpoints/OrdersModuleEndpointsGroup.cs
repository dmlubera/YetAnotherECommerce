using FastEndpoints;

namespace YetAnotherECommerce.Modules.Orders.Api.Endpoints;

public sealed class OrdersModuleEndpointsGroup : Group
{
    public OrdersModuleEndpointsGroup()
    {
        Configure("orders-module", _ => {});
    }
}