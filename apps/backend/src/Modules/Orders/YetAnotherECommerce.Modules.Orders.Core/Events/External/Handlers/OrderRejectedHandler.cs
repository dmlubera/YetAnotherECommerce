using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers;

public class OrderRejectedHandler(IOrderRepository orderRepository) : IEventHandler<OrderRejected>
{
    public async Task HandleAsync(OrderRejected @event)
    {
        var order = await orderRepository.GetByIdAsync(@event.OrderId);

        if (order is null)
            throw new OrderDoesNotExistException(@event.OrderId);

        order.RejectOrder();

        await orderRepository.UpdateAsync(order);
    }
}