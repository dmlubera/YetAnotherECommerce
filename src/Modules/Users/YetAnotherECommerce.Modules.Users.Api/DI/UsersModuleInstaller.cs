using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Users.Core.DI;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Bootstrapper")]
namespace YetAnotherECommerce.Modules.Users.Api.DI
{
    internal static class UsersModuleInstaller
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services)
        {
            services.AddCore();

            return services;
        }
    }
}