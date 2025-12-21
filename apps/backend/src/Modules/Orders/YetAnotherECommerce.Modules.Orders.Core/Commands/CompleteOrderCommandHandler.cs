using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Orders.Core.Commands;

public class CompleteOrderCommandHandler(
    IOrderRepository orderRepository,
    ILogger<CompleteOrderCommandHandler> logger)
    : ICommandHandler<CompleteOrderCommand>
{
    public async Task HandleAsync(CompleteOrderCommand command)
    {
        var order = await orderRepository.GetByIdAsync(command.OrderId);

        if (order is null)
            throw new OrderDoesNotExistException(command.OrderId);

        order.CompleteOrder();

        await orderRepository.UpdateAsync(order);

        logger.LogInformation($"Order with ID: {order.Id} has been completed.");
    }
}