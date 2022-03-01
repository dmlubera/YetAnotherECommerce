using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Orders.Core.DI;
using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Orders.Api
{
    internal class OrderModule : IModule
    {
        public const string BasePath = "orders-module";
        public string Name { get; } = "Orders";
        public string Path => BasePath;

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}