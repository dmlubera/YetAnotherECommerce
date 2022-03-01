using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherECommerce.Modules.Users.Core.DI;
using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Users.Api
{
    internal class UsersModule : IModule
    {
        public const string BasePath = "users-module";
        public string Name { get; } = "Users";
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