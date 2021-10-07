﻿using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangeEmail;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Repositories;
using YetAnotherECommerce.Modules.Identity.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Identity.Api")]
namespace YetAnotherECommerce.Modules.Identity.Core.DI
{
    internal static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<SignUpCommand>, SignUpCommandHandler>();
            services.AddTransient<ICommandHandler<SignInCommand>, SignInCommandHandler>();
            services.AddTransient<ICommandHandler<ChangeEmailCommand>, ChangeEmailCommandHandler>();
            services.AddTransient<ICommandHandler<ChangePasswordCommand>, ChangePasswordCommandHandler>();
            services.AddTransient<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}