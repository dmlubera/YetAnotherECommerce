using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Carts.Messages.Events;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers
{
    public class OrderPlacedHandler : IEventHandler<OrderPlaced>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderPlacedHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(OrderPlaced @event)
        {
            var orderItems = new List<OrderItem>();
            foreach(var item in @event.Products)
            {
                orderItems.Add(new OrderItem(item.ProductId, item.Name, item.UnitPrice, item.Quanitity));
            }
            var order = new Order(@event.CustomerId, orderItems);

            await _orderRepository.AddAsync(order);
        }
    }
}