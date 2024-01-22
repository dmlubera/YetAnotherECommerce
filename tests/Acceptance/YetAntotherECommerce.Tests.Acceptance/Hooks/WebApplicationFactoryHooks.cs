using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Postgres;
using YetAntotherECommerce.Tests.Acceptance;

namespace YetAnotherECommerce.Tests.Acceptance.Hooks
{
    [Binding]
    internal class WebApplicationFactoryHooks
    {
        private readonly TestApplicationFactory _factory;

        public WebApplicationFactoryHooks(TestApplicationFactory factory)
            => _factory = factory;

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            await _factory.InitializeAsync();
            InitializeDatabase();
        }

        [AfterScenario]
        public async Task AfterScenario() => await _factory.DisposeAsync();

        private void InitializeDatabase()
        {
            var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}
