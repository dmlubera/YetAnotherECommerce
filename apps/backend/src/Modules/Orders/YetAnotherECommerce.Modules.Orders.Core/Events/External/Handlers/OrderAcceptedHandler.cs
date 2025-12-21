using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers;

public class OrderAcceptedHandler(IOrderRepository orderRepository) : IEventHandler<OrderAccepted>
{
    public async Task HandleAsync(OrderAccepted @event)
    {
        var order = await orderRepository.GetByIdAsync(@event.OrderId);

        if (order is null)
            throw new OrderDoesNotExistException(@event.OrderId);

        order.AcceptOrder();

        await orderRepository.UpdateAsync(order);
    }
}