using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Users.Core.DAL.Postgres;
using YetAnotherECommerce.Modules.Users.Core.DAL.Postgres.Repositories;
using YetAnotherECommerce.Modules.Users.Core.Events.External.Models;
using YetAnotherECommerce.Modules.Users.Core.Repositories;
using YetAnotherECommerce.Shared.Infrastructure.Extensions;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Users.Api")]
namespace YetAnotherECommerce.Modules.Users.Core.DI;

internal static class CoreInstaller
{
    public static void AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<IUserRepository, PostgresUserRepository>();
        services.AddDbContext<UsersDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddHostedService(sp =>
            new ServiceBusMessageReceiver(sp.GetRequiredService<ServiceBusClient>(),
                sp,
                "identity.events",
                "users",
                new Dictionary<string, Type> { { "user.registered", typeof(UserRegistered) } }));
    }
}