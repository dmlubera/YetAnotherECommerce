using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using YetAnotherECommerce.Modules.Orders.Core.DAL.Postgres;
using YetAnotherECommerce.Modules.Orders.Core.DAL.Postgres.Repositories;
using YetAnotherECommerce.Modules.Orders.Core.Inbox;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Infrastructure.Extensions;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Orders.Api")]
namespace YetAnotherECommerce.Modules.Orders.Core.DI;

internal static class CoreInstaller 
{
    public static void AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
        services.RegisterQueriesFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<IOrderRepository, PostgresOrdersRepository>();
        services.AddTransient<ICustomerRepository, PostgresCustomersRepository>();
        services.AddScoped<IOrdersMessagePublisher, OrdersMessagePublisher>();

        services.AddDbContext<OrdersDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddHostedService<UsersEventsReceiver>();
        services.AddHostedService<CartsEventsReceiver>();
        services.AddHostedService<ProductsEventsReceiver>();
        services.AddScoped<ProcessInboxJob>();
    }
    
    public static void UseBackgroundJobs(this IApplicationBuilder app)
    {
        app.ApplicationServices
            .GetRequiredService<IRecurringJobManager>()
            .AddOrUpdate<ProcessInboxJob>(
                "orders-inbox-processor",
                job => job.ProcessAsync(),
                "0/15 * * * * *"
            );
    }
}