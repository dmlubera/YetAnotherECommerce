using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests.Extensions;

public static class ServiceCollectionExtensions
{
    public static void SetupDbContext<T>(this IServiceCollection services, string connectionString) where T : DbContext
    {
        services.RemoveDbContext<T>();
        services.AddDbContext<T>(options => options.UseNpgsql(connectionString));
        services.EnsureDbCreated<T>();
    }
    
    private static void RemoveDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<T>));
        if (descriptor is not null)
            services.Remove(descriptor);
    }

    private static void EnsureDbCreated<T>(this IServiceCollection services) where T : DbContext
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<T>();
        context.Database.EnsureCreated();
    }
}