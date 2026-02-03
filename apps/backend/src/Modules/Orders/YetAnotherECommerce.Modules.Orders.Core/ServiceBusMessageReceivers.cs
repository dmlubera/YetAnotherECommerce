using System;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;
using YetAnotherECommerce.Modules.Orders.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Abstractions.Database;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Orders.Core;

internal sealed class UsersEventsReceiver(
    IDbConnectionFactory dbConnectionFactory,
    ServiceBusClient serviceBusClient,
    TimeProvider timeProvider) : ServiceBusMessageReceiver(dbConnectionFactory, serviceBusClient, timeProvider)
{
    private static readonly Dictionary<string, Type> _eventTypeMappings = new()
    {
        { "registration.completed", typeof(RegistrationCompleted) }
    };

    protected override string TopicName => "users.events";
    protected override string SubscriptionName => "orders";
    protected override Dictionary<string, Type> EventTypeMapping => _eventTypeMappings;
    protected override string DatabaseSchema => "orders";
}

internal sealed class CartsEventsReceiver(
    IDbConnectionFactory dbConnectionFactory,
    ServiceBusClient serviceBusClient,
    TimeProvider timeProvider) : ServiceBusMessageReceiver(dbConnectionFactory, serviceBusClient, timeProvider)
{
    private static readonly Dictionary<string, Type> _eventTypeMappings = new()
    {
        { "order.placed", typeof(OrderPlaced) }
    };

    protected override string TopicName => "carts.events";
    protected override string SubscriptionName => "orders";
    protected override Dictionary<string, Type> EventTypeMapping => _eventTypeMappings;
    protected override string DatabaseSchema => "orders";
}

internal sealed class ProductsEventsReceiver(
    IDbConnectionFactory dbConnectionFactory,
    ServiceBusClient serviceBusClient,
    TimeProvider timeProvider) : ServiceBusMessageReceiver(dbConnectionFactory, serviceBusClient, timeProvider)
{
    private static readonly Dictionary<string, Type> _eventTypeMappings = new()
    {
        { "order.accepted", typeof(OrderAccepted) },
        { "order.rejected", typeof(OrderRejected) }
    };

    protected override string TopicName => "products.events";
    protected override string SubscriptionName => "orders";
    protected override Dictionary<string, Type> EventTypeMapping => _eventTypeMappings;
    protected override string DatabaseSchema => "orders";
}