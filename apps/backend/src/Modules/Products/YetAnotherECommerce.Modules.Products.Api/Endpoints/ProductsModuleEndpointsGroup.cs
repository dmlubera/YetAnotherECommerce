using FastEndpoints;

namespace YetAnotherECommerce.Modules.Products.Api.Endpoints;

public sealed class ProductsModuleEndpointsGroup : Group
{
    public ProductsModuleEndpointsGroup()
    {
        Configure("products-module", _ => {});
    }
}