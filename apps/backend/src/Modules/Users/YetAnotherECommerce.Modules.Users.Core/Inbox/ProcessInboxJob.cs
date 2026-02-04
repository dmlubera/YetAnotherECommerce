using System;
using System.Collections.Generic;
using Hangfire;
using YetAnotherECommerce.Modules.Users.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Abstractions.Database;
using YetAnotherECommerce.Shared.Infrastructure.InboxMessageProcessor;

namespace YetAnotherECommerce.Modules.Users.Core.Inbox;

[DisableConcurrentExecution(timeoutInSeconds: 60)]
internal sealed class ProcessInboxJob(
    IDbConnectionFactory dbConnectionFactory,
    IServiceProvider serviceProvider,
    TimeProvider timeProvider) : InboxMessagesProcessor(dbConnectionFactory, serviceProvider, timeProvider)
{
    private static readonly Dictionary<string, Type> _eventMapping = new()
    {
        { nameof(UserRegistered), typeof(UserRegistered) }
    };

    protected override Dictionary<string, Type> EventMapping => _eventMapping;

    protected override string DatabaseSchema => "users";
}