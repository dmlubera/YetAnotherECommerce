using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Dapper;
using Microsoft.Extensions.Hosting;
using YetAnotherECommerce.Shared.Abstractions.Database;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages;

public abstract class ServiceBusMessageReceiver(
    IDbConnectionFactory dbConnectionFactory,
    ServiceBusClient serviceBusClient,
    TimeProvider timeProvider) : BackgroundService
{
    protected abstract string TopicName { get; }
    protected abstract string SubscriptionName { get; }
    protected abstract Dictionary<string, Type> EventTypeMapping { get; }
    protected abstract string DatabaseSchema { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
       await ConsumeMessagesAsync(stoppingToken);
    }

    private async Task ConsumeMessagesAsync(CancellationToken cancellationToken)
    {
        await using var processor = serviceBusClient.CreateProcessor(TopicName, SubscriptionName, new ServiceBusProcessorOptions
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
        catch (OperationCanceledException)
        {
        }
        
        await processor.StopProcessingAsync(cancellationToken);
    }

    private async Task ProcessMessageAsync(ProcessMessageEventArgs eventArgs)
    {
        try
        {
            var eventType = ResolveEventType(eventArgs.Message.ApplicationProperties["eventType"].ToString());
            using var connection = await dbConnectionFactory.GetConnectionAsync();

            await connection.ExecuteAsync(
                $"""
                INSERT INTO "{DatabaseSchema}"."InboxMessages"
                ("Id", "OccurredOn", "Type", "Data")
                VALUES
                (@Id, @OccurredOn, @Type, @Data)
                """,
                new
                {
                    Id = Guid.NewGuid(),
                    OccurredOn = timeProvider.GetUtcNow(),
                    Type = eventType.Name,
                    Data = eventArgs.Message.Body.ToString()
                });
            
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
        => EventTypeMapping.TryGetValue(eventType, out var type) 
            ? type
            : throw new InvalidOperationException($"Unknown event type: {eventType}");
}