using FastEndpoints;

namespace YetAnotherECommerce.Modules.Customers.Api.Endpoints;

public sealed class CustomersModuleEndpointsGroup : Group
{
    public CustomersModuleEndpointsGroup()
    {
        Configure("customers-module", _ => {});
    }
}