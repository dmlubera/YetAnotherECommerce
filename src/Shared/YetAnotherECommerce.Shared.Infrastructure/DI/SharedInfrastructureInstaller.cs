using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Events;
using YetAnotherECommerce.Shared.Infrastructure.Api;
using YetAnotherECommerce.Shared.Infrastructure.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Events;
using YetAnotherECommerce.Shared.Infrastructure.Exceptions;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Bootstrapper")]
namespace YetAnotherECommerce.Shared.Infrastructure.DI
{
    internal static class SharedInfrastructureInstaller
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            //services.AddMvc();
            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeautreProvider());
                });

            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<IEventDispatcher, EventDispatcher>();

            services.Scan(x => x.FromAssemblies(assemblies)
                .AddClasses(x => x.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            return app;
        }
    }
}