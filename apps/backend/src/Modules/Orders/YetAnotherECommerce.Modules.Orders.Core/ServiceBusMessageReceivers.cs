using System;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Orders.Core;

public class UsersEventsReceiver(ServiceBusClient serviceBusClient, IServiceProvider serviceProvider)
    : ServiceBusMessageReceiver(
        serviceBusClient,
        serviceProvider,
        "users.events",
        "orders",
        new Dictionary<string, Type>
        {
            { "registration.completed", typeof(RegistrationCompleted) }
        });

public class CartsEventsReceiver(ServiceBusClient serviceBusClient, IServiceProvider serviceProvider)
    : ServiceBusMessageReceiver(
        serviceBusClient,
        serviceProvider,
        "carts.events",
        "orders",
        new Dictionary<string, Type>
        {
            { "order.placed", typeof(OrderPlaced) }
        });

public class ProductsEventsReceiver(ServiceBusClient serviceBusClient, IServiceProvider serviceProvider)
    : ServiceBusMessageReceiver(
        serviceBusClient, serviceProvider,
        "products.events",
        "orders",
        new Dictionary<string, Type>
        {
            { "order.accepted", typeof(OrderAccepted) },
            { "order.rejected", typeof(OrderRejected) }
        });