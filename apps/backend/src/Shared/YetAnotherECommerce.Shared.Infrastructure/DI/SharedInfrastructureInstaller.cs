using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Events;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.Messages;
using YetAnotherECommerce.Shared.Abstractions.Queries;
using YetAnotherECommerce.Shared.Infrastructure.Api;
using YetAnotherECommerce.Shared.Infrastructure.BuildingBlocks;
using YetAnotherECommerce.Shared.Infrastructure.Cache;
using YetAnotherECommerce.Shared.Infrastructure.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Correlation;
using YetAnotherECommerce.Shared.Infrastructure.Events;
using YetAnotherECommerce.Shared.Infrastructure.Exceptions;
using YetAnotherECommerce.Shared.Infrastructure.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Queries;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Bootstrapper")]
namespace YetAnotherECommerce.Shared.Infrastructure.DI;

internal static class SharedInfrastructureInstaller
{
    public static void AddInfrastructure(this IServiceCollection services, IEnumerable<Assembly> assemblies,
        IConfiguration configuration)
    {
        services.AddScoped<ExceptionHandlerMiddleware>();
        services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();

        services.AddHttpContextAccessor();
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeautreProvider());
            });

        services.Configure<MessagingOptions>(configuration.GetSection("Messaging"));

        services.AddMemoryCache();
        services.AddTransient<ICache, InMemoryCache>();
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();

        services.Scan(x => x.FromAssemblies(assemblies)
            .AddClasses(f => f.AssignableTo(typeof(IEventHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        AddMessageRegistry(services, assemblies);
        services.AddSingleton<IMessageClient, MessageClient>();
        services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
        services.AddSingleton<IMessageChannel, MessageChannel>();
        services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();
        services.AddHostedService<BackgroundMessageDispatcher>();

        services.AddSingleton<ICorrelationContext, CorrelationContext>();
        services.AddScoped<CorrelationMiddleware>();
    }

    public static void UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationMiddleware>();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }

    private static void AddMessageRegistry(IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var registry = new MessageRegistry();
        var types = assemblies
            .Where(x => x.FullName.StartsWith("YetAnotherECommerce"))
            .SelectMany(x => x.GetTypes())
            .ToArray();
        var eventTypes = types.Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x)).ToArray();

        services.AddSingleton<IMessageRegistry>(sp =>
        {
            var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();
            var eventDispatcherType = eventDispatcher.GetType();

            foreach (var type in eventTypes)
            {
                registry.AddRegistration(type, @event =>
                    (Task)eventDispatcherType
                        .GetMethod(nameof(eventDispatcher.PublishAsync))
                        ?.MakeGenericMethod(type)
                        .Invoke(eventDispatcher, [@event]));
            }

            return registry;
        });
    }
}