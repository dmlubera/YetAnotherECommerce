using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Shared.Infrastructure.Extensions;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Identity.Api")]
namespace YetAnotherECommerce.Modules.Identity.Core.DI
{
    internal static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}