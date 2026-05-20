using System.Reflection;
using System.Runtime.CompilerServices;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Catalog.Core.DAL.Postgres;
using YetAnotherECommerce.Modules.Catalog.Core.DAL.Postgres.Repositories;
using YetAnotherECommerce.Modules.Catalog.Core.Inbox;
using YetAnotherECommerce.Modules.Catalog.Core.Repositories;
using YetAnotherECommerce.Shared.Infrastructure.Extensions;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Catalog.Api")]
namespace YetAnotherECommerce.Modules.Catalog.Core.DI;

internal static class CoreInstaller
{
    public static void AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
        services.RegisterQueriesFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<IProductRepository, PostgresProductsRepository>();
        services.AddScoped<ICatalogMessagePublisher, CatalogMessagePublisher>();

        services.AddDbContext<CatalogDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddHostedService<OrdersEventsReceiver>();
        services.AddScoped<ProcessInboxJob>();
    }
    
    public static void UseBackgroundJobs(this IApplicationBuilder app)
    {
        app.ApplicationServices
            .GetRequiredService<IRecurringJobManager>()
            .AddOrUpdate<ProcessInboxJob>(
                "catalog-inbox-processor",
                job => job.ProcessAsync(),
                "0/15 * * * * *"
            );
    }
}