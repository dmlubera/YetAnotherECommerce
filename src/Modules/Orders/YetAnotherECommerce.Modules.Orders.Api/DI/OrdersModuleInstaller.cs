using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Orders.Core.DI;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Bootstrapper")]
namespace YetAnotherECommerce.Modules.Orders.Api.DI
{
    internal static class OrdersModuleInstaller
    {
        public static IServiceCollection AddOrdersModule(this IServiceCollection services)
        {
            services.AddCore();

            return services;
        }
    }
}