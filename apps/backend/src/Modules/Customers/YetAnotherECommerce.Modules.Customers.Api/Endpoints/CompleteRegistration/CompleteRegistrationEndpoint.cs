using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using YetAnotherECommerce.Modules.Customers.Core.Commands;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Customers.Api.Endpoints.CompleteRegistration;

public class CompleteRegistrationEndpoint(ICommandDispatcher commandDispatcher) : Endpoint<CompleteRegistrationRequest>
{
    public override void Configure()
    {
        Post("users/");
        Roles("customer");
        Group<CustomersModuleEndpointsGroup>();
    }

    public override async Task HandleAsync(CompleteRegistrationRequest req, CancellationToken ct)
    {
        var userId = User.Identity.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
        await commandDispatcher.DispatchAsync(new CompleteRegistrationCommand(userId, req.FirstName, req.LastName,
            req.Street, req.City, req.ZipCode, req.Country));

        await SendOkAsync(ct);
    }
}