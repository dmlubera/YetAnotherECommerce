using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Users.Core.DI;
using YetAnotherECommerce.Modules.Users.Core.Settings;
using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Users.Api
{
    internal class UsersModule : IModule
    {
        public const string BasePath = "users-module";
        public string Name { get; } = "Users";
        public string Path => BasePath;

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UsersModuleSettings>(configuration.GetSection(nameof(UsersModuleSettings)));
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}