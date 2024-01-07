using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Auth;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Events;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.Queries;
using YetAnotherECommerce.Shared.Infrastructure.Api;
using YetAnotherECommerce.Shared.Infrastructure.Auth;
using YetAnotherECommerce.Shared.Infrastructure.BuildingBlocks;
using YetAnotherECommerce.Shared.Infrastructure.Cache;
using YetAnotherECommerce.Shared.Infrastructure.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Correlation;
using YetAnotherECommerce.Shared.Infrastructure.Events;
using YetAnotherECommerce.Shared.Infrastructure.Exceptions;
using YetAnotherECommerce.Shared.Infrastructure.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Queries;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Bootstrapper")]
namespace YetAnotherECommerce.Shared.Infrastructure.DI
{
    internal static class SharedInfrastructureInstaller
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IEnumerable<Assembly> assemblies,
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

            var messagingOptions = new MessagingOptions();
            configuration.GetSection("Messaging").Bind(messagingOptions);

            services.AddMemoryCache();
            services.AddTransient<ICache, InMemoryCache>();
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddSingleton<IAuthManager, AuthManager>();

            services.Scan(x => x.FromAssemblies(assemblies)
                .AddClasses(x => x.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            AddMessageRegistry(services, assemblies);
            services.AddSingleton<IMessageClient, MessageClient>();
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            services.AddSingleton<IMessageChannel, MessageChannel>();
            services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();
            services.AddHostedService<BackroundMessageDispatcher>();

            services.AddSingleton<ICorrelationContext, CorrelationContext>();
            services.AddScoped<CorrelationMiddleware>();

            services.AddAutoMapper(assemblies);

            if (messagingOptions.UseAzureServiceBus)
                AddAzureServiceBus(services, configuration, assemblies);

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<CorrelationMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.ApplicationServices.GetService<IEventBus>()?.SetupAsync().GetAwaiter().GetResult();

            return app;
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
                var eventDsipatcher = sp.GetRequiredService<IEventDispatcher>();
                var eventDispatcherType = eventDsipatcher.GetType();

                foreach (var type in eventTypes)
                {
                    registry.AddRegistration(type, @event =>
                        (Task)eventDispatcherType
                            .GetMethod(nameof(eventDsipatcher.PublishAsync))
                            ?.MakeGenericMethod(type)
                            .Invoke(eventDsipatcher, new[] { @event }));
                }

                return registry;
            });
        }

        private static void AddAzureServiceBus(IServiceCollection services, IConfiguration configuration, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton(implementationFactory => new ServiceBusClient(configuration.GetSection("ServiceBusSettings:ConnectionString").Value));

            var types = assemblies
                .Where(x => x.FullName.StartsWith("YetAnotherECommerce"))
                .SelectMany(x => x.GetTypes())
                .ToArray();

            var eventHandlers = types.Where(x => x.IsClass
                                                 && x.GetInterfaces()
                                                     .Where(i => i.IsGenericType)
                                                     .Any(i => i.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                                     .ToArray();

            foreach (var eventHandler in eventHandlers)
            {
                if (eventHandler.GetCustomAttribute(typeof(ServiceBusSubscriptionAttribute)) is ServiceBusSubscriptionAttribute attr)
                {
                    var topicName = eventHandler.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandler<>)).GetGenericArguments().FirstOrDefault()?.Name;
                    var subscriptioName = attr.Name;

                    services.AddSingleton(implementationFactory =>
                    {
                        var serviceBusClient = implementationFactory.GetRequiredService<ServiceBusClient>();

                        return serviceBusClient.CreateSender(topicName);
                    });

                    services.AddSingleton(implementationFactory =>
                    {
                        var serviceBusClient = implementationFactory.GetRequiredService<ServiceBusClient>();

                        return serviceBusClient.CreateProcessor(topicName, subscriptioName, new ServiceBusProcessorOptions
                        {
                            AutoCompleteMessages = false
                        });
                    });
                }
            }

            services.AddSingleton<IEventBus, EventBus>();
        }
    }
}