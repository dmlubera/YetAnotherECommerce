using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Repositories;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Orders.Api")]
namespace YetAnotherECommerce.Modules.Orders.Core.DI
{
    internal static class CoreInstaller 
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}