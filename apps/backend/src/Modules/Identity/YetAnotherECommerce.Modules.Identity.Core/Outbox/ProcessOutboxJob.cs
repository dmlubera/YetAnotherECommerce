using System;
using System.Collections.Generic;
using Hangfire;
using YetAnotherECommerce.Modules.Identity.Core.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.Database;
using YetAnotherECommerce.Shared.Infrastructure.Outbox;

namespace YetAnotherECommerce.Modules.Identity.Core.Outbox;

[DisableConcurrentExecution(timeoutInSeconds: 60)]
internal sealed class ProcessOutboxJob(
    IDbConnectionFactory dbConnectionFactory,
    IDomainEventDispatcher domainEventDispatcher,
    TimeProvider timeProvider) : OutboxMessagesProcessor(dbConnectionFactory, domainEventDispatcher, timeProvider)
{
    private static readonly Dictionary<string, Type> _domainEventMapping = new()
    {
        { nameof(UserRegistered), typeof(UserRegistered) }
    };

    protected override Dictionary<string, Type> DomainEventMapping => _domainEventMapping;

    protected override string DatabaseSchema => "identity";
}