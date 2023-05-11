using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Postgres;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Postgres.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.DomainServices;
using YetAnotherECommerce.Modules.Identity.Core.Helpers;
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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, PostgresUserRepository>();
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddDbContext<IdentityDbContext>(x => x.UseNpgsql("Host=localhost;Database=YetAnotherECommerce;Username=postgres;Password=root"));
            
            return services;
        }
    }
}