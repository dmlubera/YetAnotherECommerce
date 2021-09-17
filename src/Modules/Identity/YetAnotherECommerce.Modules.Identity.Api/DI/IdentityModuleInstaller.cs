using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Identity.Core.DI;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Bootstrapper")]
namespace YetAnotherECommerce.Modules.Identity.Api.DI
{
    internal static class IdentityModuleInstaller
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services)
        {
            services.AddTransient<IMongoClient>(sp =>
            {
                var mongoSettings = sp.GetRequiredService<IOptions<IdentityModuleMongoSettings>>().Value;
                return new MongoClient(mongoSettings.ConnectionString);
            });
            services.AddCore();

            return services;
        }
    }
}