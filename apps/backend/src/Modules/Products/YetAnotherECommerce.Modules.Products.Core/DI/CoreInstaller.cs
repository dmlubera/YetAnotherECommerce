using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using YetAnotherECommerce.Modules.Products.Core.DAL.Postgres;
using YetAnotherECommerce.Modules.Products.Core.DAL.Postgres.Repositories;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Infrastructure.Extensions;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Modules.Products.Api")]
namespace YetAnotherECommerce.Modules.Products.Core.DI;

internal static class CoreInstaller
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
        services.RegisterQueriesFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<IProductRepository, PostgresProductsRepository>();
        services.AddDbContext<ProductsDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("Default")));

        return services;
    }
}