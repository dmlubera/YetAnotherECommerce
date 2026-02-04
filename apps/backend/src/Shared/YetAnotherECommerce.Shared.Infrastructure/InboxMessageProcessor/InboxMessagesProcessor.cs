using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.Inbox;
using YetAnotherECommerce.Shared.Abstractions.Database;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Shared.Infrastructure.InboxMessageProcessor;

public abstract class InboxMessagesProcessor(
    IDbConnectionFactory dbConnectionFactory,
    IServiceProvider serviceProvider,
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
            var eventType = EventMapping[message.Type];
            var @event = (IEvent)JsonSerializer.Deserialize(message.Data, eventType);

            using var scope = serviceProvider.CreateScope();
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);

            await ((Task)handlerType.GetMethod(nameof(IEventHandler<>.HandleAsync))?.Invoke(handler, [@event]))!;

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