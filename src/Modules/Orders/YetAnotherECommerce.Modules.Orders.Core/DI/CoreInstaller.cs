using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Orders.Core.DAL.Postgres;
using YetAnotherECommerce.Modules.Orders.Core.DAL.Postgres.Repositories;
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
            services.AddTransient<IOrderRepository, PostgresOrdersRepository>();
            services.AddTransient<ICustomerRepository, PostgresCustomersRepository>();

            services.AddDbContext<OrdersDbContext>(x => x.UseNpgsql("Host=localhost;Database=YetAnotherECommerce;Username=postgres;Password=root"));

            return services;
        }
    }
}