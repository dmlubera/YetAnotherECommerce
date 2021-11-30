using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Orders.Core.Commands
{
    public class RevokeOrderCommandHandler : ICommandHandler<RevokeOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageBroker _messageBroker;

        public RevokeOrderCommandHandler(IOrderRepository orderRepository, IMessageBroker messageBroker)
        {
            _orderRepository = orderRepository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(RevokeOrderCommand command)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            if (order is null)
                throw new OrderDoesNotExistException(command.OrderId);

            order.UpdateStatus(OrderStatus.Rejected);
            await _orderRepository.UpdateAsync(order);

            var orderCanceled = new OrderRevoked(command.OrderId, order.OrderItems.ToDictionary(x => x.ProductId, x => x.Quantity));
            await _messageBroker.PublishAsync(orderCanceled);
        }
    }
}