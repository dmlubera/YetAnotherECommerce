using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Carts.Api")]
namespace YetAnotherECommerce.Modules.Carts.Core.DI
{
    internal static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
            => services;
    }
}