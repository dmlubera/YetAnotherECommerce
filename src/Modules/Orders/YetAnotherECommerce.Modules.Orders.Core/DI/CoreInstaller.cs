using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Repositories;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Infrastructure.Extensions;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Orders.Api")]
namespace YetAnotherECommerce.Modules.Orders.Core.DI
{
    internal static class CoreInstaller 
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
            services.RegisterQueriesFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}