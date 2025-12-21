using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers;

public class RegistrationCompletedHandler(ICustomerRepository customerRepository)
    : IEventHandler<RegistrationCompleted>
{
    public async Task HandleAsync(RegistrationCompleted @event)
    {
        await customerRepository.AddAsync(
            new Customer(@event.CustomerId, @event.FirstName, @event.LastName, @event.Email, @event.Address));
    }
}