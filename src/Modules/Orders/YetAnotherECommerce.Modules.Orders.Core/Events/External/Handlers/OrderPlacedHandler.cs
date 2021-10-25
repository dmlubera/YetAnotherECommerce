using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Carts.Messages.Events;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Modules.Orders.Messages.Events;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers
{
    public class OrderPlacedHandler : IEventHandler<OrderPlaced>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public OrderPlacedHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository,
            IEventDispatcher eventDispatcher)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(OrderPlaced @event)
        {
            var customer = await _customerRepository.GetByIdAsync(@event.CustomerId);
            if (customer is null)
                throw new CustomerWithGivenIdDoesNotExistsException(@event.CustomerId);

            var orderItems = new List<OrderItem>();
            var productsWithQuantity = new Dictionary<Guid, int>();
            foreach(var item in @event.Products)
            {
                orderItems.Add(new OrderItem(item.ProductId, item.Name, item.UnitPrice, item.Quanitity));
                productsWithQuantity.Add(item.ProductId, item.Quanitity);
            }
            var order = new Order(@event.CustomerId, orderItems);

            await _orderRepository.AddAsync(order);

            await _eventDispatcher.PublishAsync(new OrderCreated(order.Id, productsWithQuantity));
        }
    }
}