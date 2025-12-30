using System;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;
using YetAnotherECommerce.Modules.Products.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Products.Core;

public class OrdersEventsReceiver(ServiceBusClient serviceBusClient, IServiceProvider serviceProvider)
    : ServiceBusMessageReceiver(
        serviceBusClient,
        serviceProvider,
        "orders.events",
        "products",
        new Dictionary<string, Type>
        {
            { "order.canceled", typeof(OrderCanceled) },
            { "order.revoked", typeof(OrderRevoked) },
            { "order.created", typeof(OrderCreated) }
        });