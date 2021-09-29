using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Carts.Core.DI;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Bootstrapper")]
namespace YetAnotherECommerce.Modules.Carts.Api.DI
{
    internal static class CartsModuleInstaller
    {
        public static IServiceCollection AddCartsModule(this IServiceCollection services)
            => services.AddCore();
    }
}