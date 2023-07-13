using Microsoft.EntityFrameworkCore;
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
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IUserRepository, PostgresUserRepository>();
            services.AddDbContext<UsersDbContext>(x => x.UseNpgsql("Host=localhost;Database=YetAnotherECommerce;Username=postgres;Password=root"));

            return services;
        }
    }
}