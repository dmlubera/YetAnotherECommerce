using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
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
    }
}