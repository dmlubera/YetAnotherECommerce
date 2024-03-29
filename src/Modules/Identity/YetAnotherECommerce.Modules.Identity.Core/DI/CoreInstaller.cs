﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Postgres;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Postgres.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.DomainServices;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Shared.Infrastructure.Extensions;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Identity.Api")]
namespace YetAnotherECommerce.Modules.Identity.Core.DI
{
    internal static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, PostgresUserRepository>();
            services.AddDbContext<IdentityDbContext>(x => x.UseNpgsql(configuration.GetSection("IdentityModuleSettings:DatabaseConnectionString").Value));
            
            return services;
        }
    }
}