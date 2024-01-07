using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Orders.Core.DI;
using YetAnotherECommerce.Modules.Orders.Core.Settings;
using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Orders.Api
{
    internal class OrdersModule : IModule
    {
        public const string BasePath = "orders-module";
        public string Name { get; } = "Orders";
        public string Path => BasePath;

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OrdersModuleSettings>(configuration.GetSection(nameof(OrdersModuleSettings)));
            services.AddCore(configuration);
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}