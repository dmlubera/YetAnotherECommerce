using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    internal class EventBus : IEventBus
    {
        private readonly IEnumerable<ServiceBusSender> _serviceBusSenders;
        private readonly IEnumerable<ServiceBusProcessor> _serviceBusProcessors;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EventBus> _logger;

        public EventBus(IEnumerable<ServiceBusSender> serviceBusSenders,
            IEnumerable<ServiceBusProcessor> serviceBusProcessors, IServiceProvider serviceProvider,
            ILogger<EventBus> logger)
        {
            _serviceBusSenders = serviceBusSenders;
            _serviceBusProcessors = serviceBusProcessors;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task PublishAsync(IEvent @event)
        {
            throw new NotImplementedException();
        }

        public async Task SetupAsync()
        {
            foreach (var processor in _serviceBusProcessors)
            {
                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;

                await processor.StartProcessingAsync();
            }
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var types = assemblies
                .Where(x => x.FullName.StartsWith("YetAnotherECommerce"))
                .SelectMany(x => x.GetTypes())
                .ToArray();

            var eventHandlers = types.Where(x => x.IsClass
                                                 && x.GetInterfaces()
                                                     .Where(i => i.IsGenericType)
                                                     .Any(i => i.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                                     .ToArray();

            var eventTypeName = args.Message.Subject;
            var subscriptionName = args.EntityPath.Split('/').Last();
            var handler = eventHandlers.FirstOrDefault(x => x.GetInterfaces()[0].GetGenericArguments()[0].Name.Equals(eventTypeName, StringComparison.InvariantCultureIgnoreCase)
                                                            && x.GetCustomAttribute(typeof(ServiceBusSubscriptionAttribute)) is ServiceBusSubscriptionAttribute attr
                                                            && attr.Name == subscriptionName);

            using var scope = _serviceProvider.CreateScope();
            var eventType = handler.GetInterfaces()[0].GetGenericArguments()[0];
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
            var handlerInstance = scope.ServiceProvider.GetService(handlerType);

            await (Task)handlerType.GetMethod(nameof(IEventHandler<IEvent>.HandleAsync))
                .Invoke(handlerInstance, new[] { DeserializeMessagePayload(args.Message.Body, eventType) });

            await args.CompleteMessageAsync(args.Message);
        }

        private async Task ErrorHandler(ProcessErrorEventArgs args)
        {
            await Task.CompletedTask;
        }

        private static object DeserializeMessagePayload(BinaryData body, Type type)
        {
            var payload = JObject.Parse(Encoding.UTF8.GetString(body))["payload"];

            return payload.ToObject(type);
        }
    }
}
