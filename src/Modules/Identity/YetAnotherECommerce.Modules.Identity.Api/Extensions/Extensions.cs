using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Bootstrapper")]
namespace YetAnotherECommerce.Modules.Identity.Api.Extensions
{
    internal static class Extensions
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services)
            => services;
    }
}