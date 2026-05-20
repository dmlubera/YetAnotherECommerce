using System;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;
using YetAnotherECommerce.Modules.Carts.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Abstractions.Database;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Carts.Core;

internal sealed class CatalogEventsReceiver(
    IDbConnectionFactory dbConnectionFactory,
    ServiceBusClient serviceBusClient,
    TimeProvider timeProvider) : ServiceBusMessageReceiver(dbConnectionFactory, serviceBusClient, timeProvider)
{
    private static readonly Dictionary<string, Type> _eventTypeMappings = new()
    {
        { "product.added.to.cart", typeof(ProductAddedToCart) }
    };

    protected override string TopicName => "catalog.events";
    protected override string SubscriptionName => "carts";
    protected override Dictionary<string, Type> EventTypeMapping => _eventTypeMappings;
    protected override string DatabaseSchema => "carts";
}