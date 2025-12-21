using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Shared.Infrastructure.BuildingBlocks;

internal class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    public async Task DispatchAsync(params IDomainEvent[] events)
    {
        if (events is null || !events.Any())
            return;

        using var scope = serviceProvider.CreateScope();

        foreach (var @event in events)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
            var handlers = scope.ServiceProvider.GetServices(handlerType);

            var tasks = handlers.Select(x =>
                (Task)handlerType.GetMethod(nameof(IDomainEventHandler<>.HandleAsync))
                    ?.Invoke(x, [@event]));

            await Task.WhenAll(tasks);
        }
    }
}