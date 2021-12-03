using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Shared.Infrastructure.Extensions
{
    public static class CqrsExtensions
    {
        public static IServiceCollection RegisterCommandsFromAssembly(this IServiceCollection services, Assembly assembly)
            => services.Scan(x => x.FromAssemblies(new[] { assembly })
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        public static IServiceCollection RegisterQueriesFromAssembly(this IServiceCollection services, Assembly assembly)
            => services.Scan(x => x.FromAssemblies(new[] { assembly })
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
    }
}