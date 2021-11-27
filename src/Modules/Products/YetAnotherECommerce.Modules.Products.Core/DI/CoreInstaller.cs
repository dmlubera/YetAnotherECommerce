using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Repositories;
using YetAnotherECommerce.Modules.Products.Core.DTOs;
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
            services.AddTransient<ICommandHandler<AddProductToCartCommand>, AddProductToCartCommandHandler>();
            services.AddTransient<ICommandHandler<DeleteProductCommand>, DeleteProductCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateQuantityCommand>, UpdateQuantityCommandHandler>();
            services.AddTransient<IQueryHandler<BrowseProductsQuery, IEnumerable<ProductDto>>, BrowseProductsQueryHandler>();
            services.AddTransient<IQueryHandler<GetProductDetailsQuery, ProductDetailsDto>, GetProductDetailsQueryHandler>();
            services.AddTransient<IProductRepository, ProductRepository>();

            return services;
        }
    }
}