using FastEndpoints;

namespace YetAnotherECommerce.Modules.Users.Api.Endpoints;

public sealed class UsersModuleEndpointsGroup : Group
{
    public UsersModuleEndpointsGroup()
    {
        Configure("users-module", _ => {});
    }
}