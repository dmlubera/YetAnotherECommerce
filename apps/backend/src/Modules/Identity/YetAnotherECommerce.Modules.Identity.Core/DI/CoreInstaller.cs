using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Identity.Core.DAL;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Services;
using YetAnotherECommerce.Shared.Infrastructure.Extensions;
using YetAnotherECommerce.Shared.Infrastructure.Interceptors;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Identity.Api")]
namespace YetAnotherECommerce.Modules.Identity.Core.DI;

internal static class CoreInstaller
{
    public static void AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<InsertOutboxMessagesInterceptor>();
        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<IdentityDbContext>();
        services.AddDbContext<IdentityDbContext>((serviceProvider, options) =>
            options.UseNpgsql(configuration.GetConnectionString("Default"))
                .AddInterceptors(serviceProvider.GetRequiredService<InsertOutboxMessagesInterceptor>()));

        services.AddScoped<IAuthManager, AuthManager>();
        services.AddScoped<UserManager<User>>();
        services.AddScoped<SignInManager<User>>();
        services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
        services.DecorateCommandWithUnitOfWork<IdentityDbContext>(Assembly.GetExecutingAssembly());
        services.AddScoped<IIdentityMessagePublisher, IdentityMessagePublisher>();
    }
}