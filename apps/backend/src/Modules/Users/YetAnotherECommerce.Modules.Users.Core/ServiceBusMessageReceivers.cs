using System;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;
using YetAnotherECommerce.Modules.Users.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Users.Core;

public class IdentityEventsReceiver(ServiceBusClient serviceBusClient, IServiceProvider serviceProvider)
    : ServiceBusMessageReceiver(
        serviceBusClient,
        serviceProvider,
        "identity.events",
        "users",
        new Dictionary<string, Type>
        {
            { "user.registered", typeof(UserRegistered) }
        });