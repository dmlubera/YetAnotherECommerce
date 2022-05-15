using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Carts.Core.Services;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Carts.Api")]
namespace YetAnotherECommerce.Modules.Carts.Core.DI
{
    internal static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<ICartService, CartService>();

            return services;
        }
    }
}