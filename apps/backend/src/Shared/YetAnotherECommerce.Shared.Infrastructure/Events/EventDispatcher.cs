using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Shared.Infrastructure.Events;

internal class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    public async Task DispatchAsync(IEvent @event)
    {
        if (@event is null)
            return;

        using var scope = serviceProvider.CreateScope();
        var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        await ((Task)handlerType.GetMethod(nameof(IEventHandler<>.HandleAsync))?.Invoke(handler, [@event]))!;
    }
}