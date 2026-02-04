using System;
using System.Collections.Generic;
using Hangfire;
using YetAnotherECommerce.Modules.Carts.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Abstractions.Database;
using YetAnotherECommerce.Shared.Abstractions.Events;
using YetAnotherECommerce.Shared.Infrastructure.InboxMessageProcessor;

namespace YetAnotherECommerce.Modules.Carts.Core.Inbox;

[DisableConcurrentExecution(timeoutInSeconds: 60)]
internal sealed class ProcessInboxJob(
    IDbConnectionFactory dbConnectionFactory,
    IEventDispatcher eventDispatcher,
    TimeProvider timeProvider) : InboxMessagesProcessor(dbConnectionFactory, eventDispatcher, timeProvider)
{
    private static readonly Dictionary<string, Type> _eventMapping = new()
    {
        { nameof(ProductAddedToCart), typeof(ProductAddedToCart) }
    };

    protected override Dictionary<string, Type> EventMapping => _eventMapping;

    protected override string DatabaseSchema => "carts";
}