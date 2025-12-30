using System;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;
using YetAnotherECommerce.Modules.Carts.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Carts.Core;

public class ProductsEventsReceiver(ServiceBusClient serviceBusClient, IServiceProvider serviceProvider)
    : ServiceBusMessageReceiver(
        serviceBusClient,
        serviceProvider,
        "products.events",
        "carts",
        new Dictionary<string, Type>
        {
            { "product.added.to.cart", typeof(ProductAddedToCart) } 
        });