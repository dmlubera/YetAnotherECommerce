using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Postgres;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Helpers;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;
using YetAntotherECommerce.Tests.Acceptance;

namespace YetAnotherECommerce.Tests.Acceptance.Hooks
{
    [Binding]
    internal class WebApplicationFactoryHooks
    {
        internal (string Email, string Password) PredefinedUserCredentials = ("admin@yetanotherecommerce.com", "Super$ecret");
        
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
            dbContext.Database.Migrate();
            
            var encryper = scope.ServiceProvider.GetRequiredService<IEncrypter>();
            var salt = encryper.GetSalt();
            var hash = encryper.GetHash(PredefinedUserCredentials.Password, salt);

            var user = User.Create(PredefinedUserCredentials.Email, Password.Create(hash, salt), Role.Admin);
            dbContext.Add(user);
            dbContext.SaveChanges();
        }
    }
}
