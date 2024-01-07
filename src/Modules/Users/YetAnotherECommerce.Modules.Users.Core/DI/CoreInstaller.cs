using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Users.Core.DAL.Postgres;
using YetAnotherECommerce.Modules.Users.Core.DAL.Postgres.Repositories;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Shared.Infrastructure.Extensions;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Users.Api")]
namespace YetAnotherECommerce.Modules.Users.Core.DI
{
    internal static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IUserRepository, PostgresUserRepository>();
            services.AddDbContext<UsersDbContext>(x => x.UseNpgsql(configuration.GetSection("UsersModuleSettings:DatabaseConnectionString").Value));

            return services;
        }
    }
}