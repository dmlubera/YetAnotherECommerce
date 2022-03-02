using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Products.Core.DI;
using YetAnotherECommerce.Modules.Products.Core.Settings;
using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Products.Api
{
    internal class ProductsModule : IModule
    {
        public const string BasePath = "products-module";
        public string Name { get; } = "Products";
        public string Path => BasePath;

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ProductsModuleSettings>(configuration.GetSection(nameof(ProductsModuleSettings)));
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}