using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers
{
    [ServiceBusSubscription("ordersmodule")]
    public class RegistrationCompletedHandler : IEventHandler<RegistrationCompleted>
    {
        private readonly ICustomerRepository _customerRepository;

        public RegistrationCompletedHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task HandleAsync(RegistrationCompleted @event)
        {
            await _customerRepository.AddAsync(
                new Customer(@event.CustomerId, @event.FirstName, @event.LastName, @event.Email, @event.Address));
        }
    }
}