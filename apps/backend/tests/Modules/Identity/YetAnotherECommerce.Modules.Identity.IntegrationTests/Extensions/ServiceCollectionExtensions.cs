using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public void SetupDbContext<T>(string connectionString, Action<DbContext, bool>? seed = null) where T : DbContext
        {
            services.RemoveDbContext<T>();
            services.AddDbContext<T>(options => 
                options
                    .UseNpgsql(connectionString)
                    .UseSeeding(seed ?? ((_, _) => {}) )
            );
            services.EnsureDbCreated<T>();
        }

        private void RemoveDbContext<T>() where T : DbContext
        {
            var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<T>));
            if (descriptor is not null)
                services.Remove(descriptor);
        }

        private void EnsureDbCreated<T>() where T : DbContext
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<T>();
            context.Database.EnsureCreated();
        }
    }
}