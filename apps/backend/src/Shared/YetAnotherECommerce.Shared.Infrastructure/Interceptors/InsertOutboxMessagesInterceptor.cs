using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.Outbox;

namespace YetAnotherECommerce.Shared.Infrastructure.Interceptors;

public class InsertOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
            InsertOutboxMessages(eventData.Context);
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void InsertOutboxMessages(DbContext context)
    {
        var outboxMessages = context
            .ChangeTracker
            .Entries<IHasDomainEvents>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.DomainEvents.ToList();
                entity.ClearDomainEvents();
                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage(
                Guid.NewGuid(),
                DateTime.UtcNow,
                domainEvent.GetType().Name,
                JsonSerializer.Serialize(domainEvent, domainEvent.GetType())))
            .ToList();

        if (outboxMessages.Count > 0)
            context.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}