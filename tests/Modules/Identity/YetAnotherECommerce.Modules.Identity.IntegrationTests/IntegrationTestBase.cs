using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Postgres;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Helpers;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests
{
    public abstract class IntegrationTestBase : IClassFixture<IdentityModuleWebApplicationFactory>
    {
        internal readonly ICommandDispatcher CommandDispatcher;
        internal readonly IdentityDbContext IdentityDbContext;
        internal readonly HttpClient HttpClient;
        internal readonly (string Email, string Password) PredefinedUserCredentials = ("test-customer@test.com", "Super$ecret");

        private readonly IdentityModuleWebApplicationFactory _factory;

        public IntegrationTestBase(IdentityModuleWebApplicationFactory factory)
        {
            _factory =  factory;
            var scope = _factory.Services.CreateScope();

            CommandDispatcher = scope.ServiceProvider.GetRequiredService<ICommandDispatcher>();
            IdentityDbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
            IdentityDbContext.Database.Migrate();
            InitializeDatabase().GetAwaiter().GetResult();

            HttpClient = _factory.CreateClient();
        }

        public async Task InitializeDatabase()
        {
            var scope = _factory.Services.CreateScope();
            var encrypter = scope.ServiceProvider.GetRequiredService<IEncrypter>();
            var salt = encrypter.GetSalt();
            var hash = encrypter.GetHash(PredefinedUserCredentials.Password, salt);

            var testCustomer = User.Create(PredefinedUserCredentials.Email, Password.Create(hash, salt), Role.Customer);
            IdentityDbContext.Add(testCustomer);
            await IdentityDbContext.SaveChangesAsync();
        }

    }
}
