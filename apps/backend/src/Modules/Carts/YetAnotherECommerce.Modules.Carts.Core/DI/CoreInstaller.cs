using System.Runtime.CompilerServices;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Carts.Core.Inbox;
using YetAnotherECommerce.Modules.Carts.Core.Services;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Carts.Api")]
namespace YetAnotherECommerce.Modules.Carts.Core.DI;

internal static class CoreInstaller
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddTransient<ICartService, CartService>();
        services.AddScoped<ICartsMessagePublisher, CartsMessagePublisher>();

        services.AddHostedService<ProductsEventsReceiver>();
        services.AddScoped<ProcessInboxJob>();
    }
    
    public static void UseBackgroundJobs(this IApplicationBuilder app)
    {
        app.ApplicationServices
            .GetRequiredService<IRecurringJobManager>()
            .AddOrUpdate<ProcessInboxJob>(
                "carts-inbox-processor",
                job => job.ProcessAsync(),
                "0/15 * * * * *"
            );
    }
}