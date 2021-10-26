using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Modules.Orders.Messages.Events;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Commands
{
    public class RevokeOrderCommandHandler : ICommandHandler<RevokeOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public RevokeOrderCommandHandler(IOrderRepository orderRepository, IEventDispatcher eventDispatcher)
        {
            _orderRepository = orderRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(RevokeOrderCommand command)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            if (order is null)
                throw new OrderDoesNotExistException(command.OrderId);

            order.UpdateStatus(OrderStatus.Rejected);
            await _orderRepository.UpdateAsync(order);

            var orderCanceled = new OrderRevoked(command.OrderId, order.OrderItems.ToDictionary(x => x.ProductId, x => x.Quantity));
            await _eventDispatcher.PublishAsync(orderCanceled);
        }
    }
}