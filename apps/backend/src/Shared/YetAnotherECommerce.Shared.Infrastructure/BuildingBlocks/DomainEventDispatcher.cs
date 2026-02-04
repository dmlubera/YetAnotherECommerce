using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Shared.Infrastructure.BuildingBlocks;

internal class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    public async Task DispatchAsync(IDomainEvent @event)
    {
        if (@event is null)
            return;

        using var scope = serviceProvider.CreateScope();
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        await ((Task)handlerType.GetMethod(nameof(IDomainEventHandler<>.HandleAsync))?.Invoke(handler, [@event]))!;
    }
}