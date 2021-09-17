﻿using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Users.Core.DAL.Mongo.Repositories;
using YetAnotherECommerce.Modules.Users.Core.Repositories;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Users.Api")]
namespace YetAnotherECommerce.Modules.Users.Core.DI
{
    internal static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}