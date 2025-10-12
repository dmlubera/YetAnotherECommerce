using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Orders.Core.Commands
{
    public class CompleteOrderCommandHandler : ICommandHandler<CompleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CompleteOrderCommandHandler> _logger;

        public CompleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<CompleteOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task HandleAsync(CompleteOrderCommand command)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            if (order is null)
                throw new OrderDoesNotExistException(command.OrderId);

            order.CompleteOrder();

            await _orderRepository.UpdateAsync(order);

            _logger.LogInformation($"Order with ID: {order.Id} has been completed.");
        }
    }
}