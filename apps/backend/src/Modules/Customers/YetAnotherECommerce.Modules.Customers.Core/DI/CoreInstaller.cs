using System.Reflection;
using System.Runtime.CompilerServices;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Customers.Core.DAL.Postgres;
using YetAnotherECommerce.Modules.Customers.Core.DAL.Postgres.Repositories;
using YetAnotherECommerce.Modules.Customers.Core.Inbox;
using YetAnotherECommerce.Modules.Customers.Core.Repositories;
using YetAnotherECommerce.Shared.Infrastructure.Extensions;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Customers.Api")]
namespace YetAnotherECommerce.Modules.Customers.Core.DI;

internal static class CoreInstaller
{
    public static void AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<IUserRepository, PostgresUserRepository>();
        services.AddScoped<ICustomersMessagePublisher, CustomersMessagePublisher>();

        services.AddDbContext<CustomersDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddHostedService<IdentityEventsReceiver>();
        services.AddScoped<ProcessInboxJob>();
    }
    
    public static void UseBackgroundJobs(this IApplicationBuilder app)
    {
        app.ApplicationServices
            .GetRequiredService<IRecurringJobManager>()
            .AddOrUpdate<ProcessInboxJob>(
                "customers-inbox-processor",
                job => job.ProcessAsync(),
                "0/15 * * * * *"
            );
    }
}