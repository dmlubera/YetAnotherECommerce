using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.Outbox;
using YetAnotherECommerce.Shared.Abstractions.Database;

namespace YetAnotherECommerce.Shared.Infrastructure.Outbox;

public abstract class OutboxMessagesProcessor(
    IDbConnectionFactory dbConnectionFactory,
    IDomainEventDispatcher domainEventDispatcher,
    TimeProvider timeProvider)
{
    protected abstract Dictionary<string, Type> DomainEventMapping { get; }
    protected abstract string DatabaseSchema { get; }

    public async Task ProcessAsync()
    {
        using var connection = await dbConnectionFactory.GetConnectionAsync();
        var outboxMessages = (await connection.QueryAsync<OutboxMessage>(
            $"""
             SELECT *
             FROM "{DatabaseSchema}"."OutboxMessages"
             WHERE "ProcessedDate" IS NULL
             ORDER BY "OccurredOn"
             """)).ToList();

        if (!outboxMessages.Any())
            return;

        foreach (var message in outboxMessages)
        {
            var domainEvent = (IDomainEvent)JsonSerializer.Deserialize(message.Data, DomainEventMapping[message.Type]);
            await domainEventDispatcher.DispatchAsync(domainEvent);

            await connection.ExecuteAsync(
                $"""
                 UPDATE "{DatabaseSchema}"."OutboxMessages"
                 SET "ProcessedDate" = @ProcessedDate
                 WHERE "Id" = @Id
                 """,
                new
                {
                    ProcessedDate = timeProvider.GetUtcNow(),
                    message.Id
                }
            );
        }
    }
}