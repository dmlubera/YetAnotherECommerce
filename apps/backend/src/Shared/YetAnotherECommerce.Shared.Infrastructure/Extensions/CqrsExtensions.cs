using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Shared.Infrastructure.Extensions;

public static class CqrsExtensions
{
    extension(IServiceCollection services)
    {
        public void RegisterCommandsFromAssembly(Assembly assembly)
        {
            services
                .Scan(x => x.FromAssemblies(assembly)
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime())
                .Scan(x => x.FromAssemblies(assembly)
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        }

        public void RegisterQueriesFromAssembly(Assembly assembly)
        {
            services.Scan(x => x.FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }
    }
}