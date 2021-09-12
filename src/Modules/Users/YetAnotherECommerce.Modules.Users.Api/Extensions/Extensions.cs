using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Users.Core.Extensions;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Bootstrapper")]
namespace YetAnotherECommerce.Modules.Users.Api.Extensions
{
    internal static class Extensions
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services)
        {
            services.AddCore();

            return services;
        }
    }
}