using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Users.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Users.Api.Endpoints.CompleteRegistration;

public class CompleteRegistrationEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<CompleteRegistrationRequest>
{
    public override void Configure()
    {
        Post("users/");
        Roles("customer");
        Group<UsersModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(CompleteRegistrationRequest req, CancellationToken ct)
    {
        var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
        await commandDispatcher.DispatchAsync(new CompleteRegistrationCommand(userId, req.FirstName, req.LastName,
            req.Street, req.City, req.ZipCode, req.Country));

        await SendOkAsync(ct);
    }
}