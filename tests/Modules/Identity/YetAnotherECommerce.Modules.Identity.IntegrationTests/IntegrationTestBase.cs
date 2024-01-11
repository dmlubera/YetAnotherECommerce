using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Postgres;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests
{
    public abstract class IntegrationTestBase : IClassFixture<IdentityModuleWebApplicationFactory>
    {
        internal readonly ICommandDispatcher CommandDispatcher;
        internal readonly IdentityDbContext IdentityDbContext;
        internal readonly HttpClient HttpClient;

        public IntegrationTestBase(IdentityModuleWebApplicationFactory factory)
        {
            var scope = factory.Services.CreateScope();

            CommandDispatcher = scope.ServiceProvider.GetRequiredService<ICommandDispatcher>();
            IdentityDbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
            IdentityDbContext.Database.EnsureCreated();

            HttpClient = factory.CreateClient();
        }
    }
}
