using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using Hangfire;
using YetAnotherECommerce.Modules.Identity.Core.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.Outbox;
using YetAnotherECommerce.Shared.Abstractions.Database;
using YetAnotherECommerce.Shared.Abstractions.Outbox;

namespace YetAnotherECommerce.Modules.Identity.Core.Outbox;

[DisableConcurrentExecution(timeoutInSeconds: 60)]
internal sealed class ProcessOutboxJob(IDbConnectionFactory dbConnectionFactory, IDomainEventDispatcher domainEventDispatcher) : IProcessOutboxJob
{
    private readonly Dictionary<string, Type> _domainEventMapping = new()
    {
        { nameof(UserRegistered), typeof(UserRegistered) }
    };
    
    public async Task ProcessAsync()
    {
        using var connection = await dbConnectionFactory.GetConnectionAsync();
        var outboxMessages = (await connection.QueryAsync<OutboxMessage>(
            """
            SELECT *
            FROM "identity"."OutboxMessages"
            WHERE "ProcessedDate" IS NULL
            ORDER BY "OccurredOn"
            """)).ToList();
        
        if (!outboxMessages.Any())
            return;

        foreach (var message in outboxMessages)
        {
            var domainEvent = (IDomainEvent)JsonSerializer.Deserialize(message.Data, _domainEventMapping[message.Type]);
            await domainEventDispatcher.DispatchAsync(domainEvent);

            await connection.ExecuteAsync(
                $"""
                UPDATE "identity"."OutboxMessages"
                SET "ProcessedDate" = '{DateTime.UtcNow}'
                WHERE "Id" = '{message.Id}'
                """
                );
        }
    }
}