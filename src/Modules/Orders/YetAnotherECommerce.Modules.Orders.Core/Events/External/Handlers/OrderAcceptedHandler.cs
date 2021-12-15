using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers
{
    public class OrderAcceptedHandler : IEventHandler<OrderAccepted>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderAcceptedHandler(IOrderRepository orderRepository)
            => _orderRepository = orderRepository;

        public async Task HandleAsync(OrderAccepted @event)
        {
            var order = await _orderRepository.GetByIdAsync(@event.OrderId);

            if (order is null)
                throw new OrderDoesNotExistException(@event.OrderId);

            order.UpdateStatus(OrderStatus.Accepted);

            await _orderRepository.UpdateAsync(order);
        }
    }
}