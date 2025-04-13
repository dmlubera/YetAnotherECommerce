using FastEndpoints;

namespace YetAnotherECommerce.Modules.Identity.Api.Endpoints;

public sealed class IdentityModuleEndpointsGroup : Group
{
    public IdentityModuleEndpointsGroup()
    {
        Configure("identity-module", _ => {});
    }
}