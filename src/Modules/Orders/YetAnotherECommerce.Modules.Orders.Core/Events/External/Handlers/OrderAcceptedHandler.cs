using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Modules.Products.Messages.Events;
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
            order.UpdateStatus("Accepted");

            await _orderRepository.UpdateAsync(order);
        }
    }
}