using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Carts.Core.Events.External.Handlers;
using YetAnotherECommerce.Modules.Products.Messages.Events;
using YetAnotherECommerce.Shared.Abstractions.Events;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Carts.Api")]
namespace YetAnotherECommerce.Modules.Carts.Core.DI
{
    internal static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IEventHandler<ProductAddedToCart>, ProductAddedToCartHandler>();

            return services;
        }
    }
}