using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Xunit;
using YetAnotherECommerce.Bootstrapper;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Postgres;

namespace YetAntotherECommerce.Tests.Acceptance
{
    internal class TestApplicationFactory : WebApplicationFactory<Startup>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer;

        public TestApplicationFactory()
        {
            _dbContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase("YetAnotherECommerce")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
            => builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<IdentityDbContext>));
                if (descriptor is not null)
                    services.Remove(descriptor);

                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseNpgsql(_dbContainer.GetConnectionString()));
            });

        public async Task InitializeAsync() => await _dbContainer.StartAsync();

        public new async Task DisposeAsync() => await _dbContainer.StopAsync();
    }
}
