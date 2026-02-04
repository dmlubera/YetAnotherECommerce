using System.Reflection;
using System.Runtime.CompilerServices;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Products.Core.DAL.Postgres;
using YetAnotherECommerce.Modules.Products.Core.DAL.Postgres.Repositories;
using YetAnotherECommerce.Modules.Products.Core.Inbox;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Infrastructure.Extensions;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Products.Api")]
namespace YetAnotherECommerce.Modules.Products.Core.DI;

internal static class CoreInstaller
{
    public static void AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
        services.RegisterQueriesFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<IProductRepository, PostgresProductsRepository>();
        services.AddScoped<IProductsMessagePublisher, ProductsMessagePublisher>();

        services.AddDbContext<ProductsDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddHostedService<OrdersEventsReceiver>();
        services.AddScoped<ProcessInboxJob>();
    }
    
    public static void UseBackgroundJobs(this IApplicationBuilder app)
    {
        app.ApplicationServices
            .GetRequiredService<IRecurringJobManager>()
            .AddOrUpdate<ProcessInboxJob>(
                "products-inbox-processor",
                job => job.ProcessAsync(),
                "0/15 * * * * *"
            );
    }
}