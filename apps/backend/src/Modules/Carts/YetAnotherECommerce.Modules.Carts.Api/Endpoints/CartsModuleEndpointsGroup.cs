using FastEndpoints;

namespace YetAnotherECommerce.Modules.Carts.Api.Endpoints;

public sealed class CartsModuleEndpointsGroup : Group
{
    public CartsModuleEndpointsGroup()
    {
        Configure("carts-module", _ => {});
    }
}