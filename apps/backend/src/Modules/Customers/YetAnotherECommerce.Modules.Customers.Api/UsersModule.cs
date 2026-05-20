using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Customers.Core.DI;
using YetAnotherECommerce.Modules.Customers.Core.Settings;
using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Customers.Api;

internal class UsersModule : IModule
{
    public const string BasePath = "users-module";
    public string Name { get; } = "Users";
    public string Path => BasePath;

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CustomersModuleSettings>(configuration.GetSection(nameof(CustomersModuleSettings)));
        services.AddCore(configuration);
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseBackgroundJobs();
    }
}