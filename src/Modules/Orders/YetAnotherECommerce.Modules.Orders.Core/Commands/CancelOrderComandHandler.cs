using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Events;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Orders.Core.Commands
{
    public class CancelOrderComandHandler : ICommandHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<CancelOrderComandHandler> _logger;

        public CancelOrderComandHandler(IOrderRepository orderRepository, IMessageBroker messageBroker,
            ILogger<CancelOrderComandHandler> logger)
        {
            _orderRepository = orderRepository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task HandleAsync(CancelOrderCommand command)
        {
            var order = await _orderRepository.GetForCustomerByIdAsync(command.CustomerId, command.OrderId);

            if (order is null)
                throw new NoSuchOrderExistsForCustomerException(command.OrderId, command.CustomerId);

            order.CancelOrder();
            await _orderRepository.UpdateAsync(order);

            var orderCanceled = new OrderCanceled(command.OrderId, order.OrderItems.ToDictionary(x => x.ProductId, x => x.Quantity));

            await _messageBroker.PublishAsync(orderCanceled);

            _logger.LogInformation("Order canceled: {@order}", orderCanceled);
        }
    }
}