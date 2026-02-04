using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Carts.Core.DI;
using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Carts.Api;

internal class CartsModule : IModule
{
    public const string BasePath = "carts-module";
    public string Name { get; } = "Carts";
    public string Path => BasePath;

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseBackgroundJobs();
    }
}