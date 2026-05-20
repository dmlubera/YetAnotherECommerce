using FastEndpoints;

namespace YetAnotherECommerce.Modules.Catalog.Api.Endpoints;

public sealed class CatalogModuleEndpointsGroup : Group
{
    public CatalogModuleEndpointsGroup()
    {
        Configure("catalog-module", _ => {});
    }
}