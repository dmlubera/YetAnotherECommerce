using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YetAnotherECommerce.Modules.Orders.Core.Events;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Orders.Core.Commands;

public class CancelOrderComandHandler(
    IOrderRepository orderRepository,
    IMessagePublisher messagePublisher,
    ILogger<CancelOrderComandHandler> logger)
    : ICommandHandler<CancelOrderCommand>
{
    public async Task HandleAsync(CancelOrderCommand command)
    {
        var order = await orderRepository.GetForCustomerByIdAsync(command.CustomerId, command.OrderId);

        if (order is null)
            throw new NoSuchOrderExistsForCustomerException(command.OrderId, command.CustomerId);

        order.CancelOrder();
        await orderRepository.UpdateAsync(order);

        var orderCanceled = new OrderCanceled(command.OrderId, order.OrderItems.ToDictionary(x => x.ProductId, x => x.Quantity));

        await messagePublisher.PublishAsync(orderCanceled);

        logger.LogInformation("Order canceled: {@order}", orderCanceled);
    }
}