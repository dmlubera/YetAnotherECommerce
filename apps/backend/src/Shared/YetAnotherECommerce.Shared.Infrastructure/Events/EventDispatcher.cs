using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Shared.Infrastructure.Events;

public class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
    {
        using var scope = serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

        var tasks = handlers.Select(x => x.HandleAsync(@event));

        await Task.WhenAll(tasks);
    }
}