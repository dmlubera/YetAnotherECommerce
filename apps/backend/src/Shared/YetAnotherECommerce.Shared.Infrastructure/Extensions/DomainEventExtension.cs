using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;

namespace YetAnotherECommerce.Shared.Infrastructure.Extensions;

public static class DomainEventExtension
{
    public static void RegisterDomainEventHandlersFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        services
            .Scan(x => x.FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
    }
}