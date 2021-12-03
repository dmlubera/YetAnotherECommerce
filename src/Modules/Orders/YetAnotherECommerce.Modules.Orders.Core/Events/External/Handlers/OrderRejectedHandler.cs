using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers
{
    public class OrderRejectedHandler : IEventHandler<OrderRejected>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderRejectedHandler(IOrderRepository orderRepository)
            => _orderRepository = orderRepository;

        public async Task HandleAsync(OrderRejected @event)
        {
            var order = await _orderRepository.GetByIdAsync(@event.OrderId);
            order.UpdateStatus(OrderStatus.Rejected);

            await _orderRepository.UpdateAsync(order);
        }
    }
}