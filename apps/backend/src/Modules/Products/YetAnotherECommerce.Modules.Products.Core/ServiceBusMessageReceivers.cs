using System;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;
using YetAnotherECommerce.Modules.Products.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Abstractions.Database;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Products.Core;

internal sealed class OrdersEventsReceiver(
    IDbConnectionFactory dbConnectionFactory,
    ServiceBusClient serviceBusClient,
    TimeProvider timeProvider)
    : ServiceBusMessageReceiver(dbConnectionFactory, serviceBusClient, timeProvider)
{
    private static readonly Dictionary<string, Type> _eventTypeMapping = new()
    {
        { "order.canceled", typeof(OrderCanceled) },
        { "order.revoked", typeof(OrderRevoked) },
        { "order.created", typeof(OrderCreated) }
    };
    
    protected override string TopicName =>  "orders.events";
    protected override string SubscriptionName => "products";
    protected override Dictionary<string, Type> EventTypeMapping => _eventTypeMapping;
    protected override string DatabaseSchema => "products";
}