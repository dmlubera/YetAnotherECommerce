using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Orders.Core.Exceptions;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;
using YetAnotherECommerce.Shared.Abstractions.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Orders.Core.Events.External.Handlers;

public class OrderPlacedHandler(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IMessageBroker messageBroker)
    : IEventHandler<OrderPlaced>
{
    public async Task HandleAsync(OrderPlaced @event)
    {
        var customer = await customerRepository.GetByIdAsync(@event.CustomerId);
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

        await orderRepository.AddAsync(order);

        await messageBroker.PublishAsync(new OrderCreated(order.Id, productsWithQuantity));
    }
}