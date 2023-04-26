using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers
{
    [ServiceBusSubscription("ordersmodule")]
    public class OrderPlacedHandler : IEventHandler<OrderPlaced>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMessageBroker _messageBroker;

        public OrderPlacedHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository,
            IMessageBroker messageBroker)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _messageBroker = messageBroker;
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
                orderItems.Add(new OrderItem(item.ProductId, item.Name, item.UnitPrice, item.Quantity));
                productsWithQuantity.Add(item.ProductId, item.Quantity);
            }
            var order = new Order(@event.CustomerId, orderItems);

            await _orderRepository.AddAsync(order);

            await _messageBroker.PublishAsync(new OrderCreated(order.Id, productsWithQuantity));
        }
    }
}