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
    public class CancelOrderComandHandler : ICommandHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public CancelOrderComandHandler(IOrderRepository orderRepository, IEventDispatcher eventDispatcher)
        {
            _orderRepository = orderRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(CancelOrderCommand command)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            if (order is null || order.CustomerId != command.CustomerId)
                throw new NoSuchOrderExistsForCustomerException(command.OrderId, command.CustomerId);

            order.UpdateStatus(OrderStatus.Canceled);
            await _orderRepository.UpdateAsync(order);

            var orderCanceled = new OrderCanceled(command.CustomerId, order.OrderItems.ToDictionary(x => x.ProductId, x => x.Quantity));

            await _eventDispatcher.PublishAsync(orderCanceled);
        }
    }
}