using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YetAnotherECommerce.Modules.Orders.Core.Events;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Orders.Core.Commands;

public class RevokeOrderCommandHandler(
    IOrderRepository orderRepository,
    IMessagePublisher messagePublisher,
    ILogger<RevokeOrderCommandHandler> logger)
    : ICommandHandler<RevokeOrderCommand>
{
    public async Task HandleAsync(RevokeOrderCommand command)
    {
        var order = await orderRepository.GetByIdAsync(command.OrderId);

        if (order is null)
            throw new OrderDoesNotExistException(command.OrderId);

        order.RejectOrder();
        await orderRepository.UpdateAsync(order);

        var orderRevoked = new OrderRevoked(command.OrderId, order.OrderItems.ToDictionary(x => x.ProductId, x => x.Quantity));
        await messagePublisher.PublishAsync(orderRevoked);

        logger.LogInformation("Order revoked: {@order}", orderRevoked);
    }
}