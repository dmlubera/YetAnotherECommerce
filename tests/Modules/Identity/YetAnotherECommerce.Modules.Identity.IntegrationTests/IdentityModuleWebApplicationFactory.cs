using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Testcontainers.PostgreSql;
using Xunit;
using YetAnotherECommerce.Bootstrapper;
using YetAnotherECommerce.Modules.Identity.Core.DAL;
using YetAnotherECommerce.Modules.Identity.IntegrationTests.Extensions;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests
{
    public class IdentityModuleWebApplicationFactory : WebApplicationFactory<Startup>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithCleanUp(true)
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
            => builder.ConfigureTestServices(services =>
            {
                services.SetupDbContext<IdentityDbContext>(_dbContainer.GetConnectionString());
            });

        public async Task InitializeAsync() => await _dbContainer.StartAsync();

        public new async Task DisposeAsync() => await _dbContainer.StopAsync();
    }
}
