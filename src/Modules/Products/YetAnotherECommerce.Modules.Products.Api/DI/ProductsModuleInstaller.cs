using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Products.Core.DI;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Bootstrapper")]
namespace YetAnotherECommerce.Modules.Products.Api.DI
{
    internal static class ProductsModuleInstaller
    {
        public static IServiceCollection AddProductsModule(this IServiceCollection services)
            => services.AddCore();
    }
}