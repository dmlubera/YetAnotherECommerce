using System;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;
using YetAnotherECommerce.Modules.Users.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Abstractions.Database;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Users.Core;

internal sealed class IdentityEventsReceiver(
    IDbConnectionFactory dbConnectionFactory,
    ServiceBusClient serviceBusClient,
    TimeProvider timeProvider) : ServiceBusMessageReceiver(dbConnectionFactory, serviceBusClient, timeProvider)
{
    private static readonly Dictionary<string, Type> _eventTypeMappings = new()
    {
        { "user.registered", typeof(UserRegistered) }
    };

    protected override string TopicName => "identity.events";
    protected override string SubscriptionName => "users";
    protected override Dictionary<string, Type> EventTypeMapping => _eventTypeMappings;
    protected override string DatabaseSchema => "users";
}