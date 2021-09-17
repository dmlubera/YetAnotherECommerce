using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Repositories;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Queries;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Queries;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Products.Api")]
namespace YetAnotherECommerce.Modules.Products.Core.DI
{
    internal static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<AddProductCommand>, AddProductCommandHandler>();
            services.AddTransient<IQueryHandler<GetAllProductsQuery, IEnumerable<Product>>, GetAllProductsQueryHandler>();
            services.AddTransient<IProductRepository, ProductRepository>();

            return services;
        }
    }
}