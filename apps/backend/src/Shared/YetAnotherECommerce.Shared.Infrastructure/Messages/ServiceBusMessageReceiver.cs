using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages;

public class ServiceBusMessageReceiver(
    ServiceBusClient serviceBusClient,
    IServiceProvider serviceProvider,
    string topicName,
    string subscriptionName,
    Dictionary<string, Type> eventTypeMap) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
       await ConsumeMessagesAsync(stoppingToken);
    }

    private async Task ConsumeMessagesAsync(CancellationToken cancellationToken)
    {
        await using var processor = serviceBusClient.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = 1
        });

        processor.ProcessMessageAsync += ProcessMessageAsync;
        processor.ProcessErrorAsync += ProcessErrorAsync;

        await processor.StartProcessingAsync(cancellationToken);

        try
        {
            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
        catch (OperationCanceledException e)
        {
        }
        
        await processor.StopProcessingAsync(cancellationToken);
    }

    private async Task ProcessMessageAsync(ProcessMessageEventArgs eventArgs)
    {
        try
        {
            var eventType = ResolveEventType(eventArgs.Message.ApplicationProperties["eventType"].ToString());
            var @event = JsonSerializer.Deserialize(eventArgs.Message.Body, eventType);

            using var scope = serviceProvider.CreateScope();
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);

            await ((Task)handlerType.GetMethod(nameof(IEventHandler<>.HandleAsync))?.Invoke(handler, [@event]))!;
            
            await eventArgs.CompleteMessageAsync(eventArgs.Message);
        }
        catch (Exception e)
        {
            await eventArgs.AbandonMessageAsync(eventArgs.Message);
        }
    }

    private Task ProcessErrorAsync(ProcessErrorEventArgs errorEventArgs)
    {
        return Task.CompletedTask;
    }

    private Type ResolveEventType(string eventType)
        => eventTypeMap.TryGetValue(eventType, out var type) 
            ? type
            : throw new InvalidOperationException($"Unknown event type: {eventType}");
}