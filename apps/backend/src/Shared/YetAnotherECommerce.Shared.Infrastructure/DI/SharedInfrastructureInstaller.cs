using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks.DomainEvents;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Database;
using YetAnotherECommerce.Shared.Abstractions.Events;
using YetAnotherECommerce.Shared.Abstractions.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.Queries;
using YetAnotherECommerce.Shared.Infrastructure.Api;
using YetAnotherECommerce.Shared.Infrastructure.BuildingBlocks;
using YetAnotherECommerce.Shared.Infrastructure.Cache;
using YetAnotherECommerce.Shared.Infrastructure.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Correlation;
using YetAnotherECommerce.Shared.Infrastructure.Db;
using YetAnotherECommerce.Shared.Infrastructure.Events;
using YetAnotherECommerce.Shared.Infrastructure.Exceptions;
using YetAnotherECommerce.Shared.Infrastructure.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Queries;
using IMessagePublisher = YetAnotherECommerce.Shared.Abstractions.Messages.IMessagePublisher;

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

        services.AddSingleton<ICorrelationContext, CorrelationContext>();
        services.AddScoped<CorrelationMiddleware>();

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(configuration.GetConnectionString("Default")));

        AddServiceBus(services, configuration);
        AddHangfire(services, configuration);
    }

    public static void UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationMiddleware>();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        app.UseHangfireDashboard(options: new DashboardOptions
        {
            Authorization = []
        });
    }

    private static void AddServiceBus(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAzureClients(builder =>
        {
            builder.AddServiceBusClient(configuration.GetSection("ServiceBusSettings:ConnectionString").Value);
        });
        
        services.AddSingleton<IMessagePublisher, ServiceBusMessagePublisher>();
    }

    private static void AddHangfire(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
            config.UsePostgreSqlStorage(options =>
                options.UseNpgsqlConnection(configuration.GetConnectionString("Default"))));

        services.AddHangfireServer(options => options.SchedulePollingInterval = TimeSpan.FromSeconds(1));
    }
}