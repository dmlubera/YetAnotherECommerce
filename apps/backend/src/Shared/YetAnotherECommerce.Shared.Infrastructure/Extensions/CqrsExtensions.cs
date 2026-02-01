using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Queries;
using YetAnotherECommerce.Shared.Infrastructure.Decorators;

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

        public void DecorateCommandWithUnitOfWork<TDbContext>(Assembly assembly)
        {
            foreach (var commandHandlerType in assembly.GetTypes().Where(t =>
                         t.GetInterfaces().Any(i =>
                             i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>))))
            {
                var interfaceType = commandHandlerType.GetInterfaces().First(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>));
                services.Decorate(interfaceType, (inner, sp) =>
                {
                    var dbContext = sp.GetRequiredService<TDbContext>();
                    var decoratorType =
                        typeof(UnitOfWorkCommandHandlerDecorator<,>).MakeGenericType(
                            interfaceType.GenericTypeArguments[0], typeof(TDbContext));
                    return ActivatorUtilities.CreateInstance(sp, decoratorType, inner, dbContext);
                });
            }

            foreach (var commandHandlerType in assembly.GetTypes().Where(t =>
                         t.GetInterfaces().Any(i =>
                             i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>))))
            {
                var interfaceType = commandHandlerType.GetInterfaces().First(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));
                services.Decorate(interfaceType, (inner, sp) =>
                {
                    var dbContext = sp.GetRequiredService<TDbContext>();
                    var decoratorType = typeof(UnitOfWorkCommandHandlerDecorator<,,>).MakeGenericType(
                        interfaceType.GenericTypeArguments[0], interfaceType.GenericTypeArguments[1],
                        typeof(TDbContext));
                    return ActivatorUtilities.CreateInstance(sp, decoratorType, inner, dbContext);
                });
            }
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