using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.Inbox;
using YetAnotherECommerce.Shared.Abstractions.Database;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Shared.Infrastructure.InboxMessageProcessor;

public abstract class InboxMessagesProcessor(
    IDbConnectionFactory dbConnectionFactory,
    IEventDispatcher eventDispatcher,
    TimeProvider timeProvider)
{
    protected abstract Dictionary<string, Type> EventMapping { get; }
    protected abstract string DatabaseSchema { get; }

    public async Task ProcessAsync()
    {
        using var connection = await dbConnectionFactory.GetConnectionAsync();
        var inboxMessages = (await connection.QueryAsync<InboxMessage>(
            $"""
             SELECT *
             FROM "{DatabaseSchema}"."InboxMessages"
             WHERE "ProcessedDate" IS NULL
             ORDER BY "OccurredOn"
             """)).ToList();

        if (!inboxMessages.Any())
            return;

        foreach (var message in inboxMessages)
        {
            var @event = (IEvent)JsonSerializer.Deserialize(message.Data, EventMapping[message.Type]);
            await eventDispatcher.DispatchAsync(@event);

            await connection.ExecuteAsync(
                $"""
                 UPDATE "{DatabaseSchema}"."InboxMessages"
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