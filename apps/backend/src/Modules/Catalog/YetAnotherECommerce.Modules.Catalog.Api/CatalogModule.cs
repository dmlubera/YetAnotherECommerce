using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Catalog.Core.DI;
using YetAnotherECommerce.Modules.Catalog.Core.Settings;
using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Catalog.Api;

internal class CatalogModule : IModule
{
    public const string BasePath = "catalog-module";
    public string Name { get; } = "Catalog";
    public string Path => BasePath;

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CatalogModuleSettings>(configuration.GetSection(nameof(CatalogModuleSettings)));
        services.AddCore(configuration);
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseBackgroundJobs();
    }
}