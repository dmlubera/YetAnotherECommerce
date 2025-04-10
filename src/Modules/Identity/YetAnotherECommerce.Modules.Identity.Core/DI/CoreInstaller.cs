using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using YetAnotherECommerce.Modules.Identity.Core.DAL;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Shared.Infrastructure.Extensions;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Identity.Api")]
namespace YetAnotherECommerce.Modules.Identity.Core.DI;

internal static class CoreInstaller
{
    public static void AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<IdentityDbContext>();
        services.AddDbContext<IdentityDbContext>(x => x.UseNpgsql(configuration.GetSection("IdentityModuleSettings:DatabaseConnectionString").Value));
        
        services.AddScoped<UserManager<User>>();
        services.AddScoped<SignInManager<User>>();
        services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
    }
}