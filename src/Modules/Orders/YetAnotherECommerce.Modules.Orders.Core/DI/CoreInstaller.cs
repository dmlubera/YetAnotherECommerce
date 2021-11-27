using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Orders.Core.Commands;
using YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Repositories;
using YetAnotherECommerce.Modules.Orders.Core.DTOs;
using YetAnotherECommerce.Modules.Orders.Core.Entities;
using YetAnotherECommerce.Modules.Orders.Core.Queries;
using YetAnotherECommerce.Modules.Orders.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Queries;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Orders.Api")]
namespace YetAnotherECommerce.Modules.Orders.Core.DI
{
    internal static class CoreInstaller 
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IQueryHandler<BrowseQuery, IList<Order>>, BrowseQueryHandler>();
            services.AddTransient<IQueryHandler<BrowseCustomerOrdersQuery, IList<OrderDto>>, BrowseCustomerOrdersQueryHandler>();
            services.AddTransient<IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto>, GetOrderDetailsQueryHandler>();
            services.AddTransient<ICommandHandler<CancelOrderCommand>, CancelOrderComandHandler>();
            services.AddTransient<ICommandHandler<CompleteOrderCommand>, CompleteOrderCommandHandler>();
            services.AddTransient<ICommandHandler<RevokeOrderCommand>, RevokeOrderCommandHandler>();

            return services;
        }
    }
}