using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Repositories;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Products.Api")]
namespace YetAnotherECommerce.Modules.Products.Core.DI
{
    internal static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<AddProductCommand>, AddProductCommandHandler>();
            services.AddTransient<IProductRepository, ProductRepository>();

            return services;
        }
    }
}