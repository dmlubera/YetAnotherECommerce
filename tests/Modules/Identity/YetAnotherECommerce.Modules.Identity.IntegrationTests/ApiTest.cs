using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.DAL;
using YetAnotherECommerce.Modules.Identity.Core.Entities;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests;

[Collection(nameof(ApiTestCollection))]
public abstract class ApiTest : IAsyncLifetime
{
    internal readonly IdentityDbContext IdentityDbContext;
    internal readonly HttpClient HttpClient;
    internal readonly (string Email, string Password) PredefinedUserCredentials = ("testcustomer@test.com", "Super$ecret1");

    private readonly IdentityModuleWebApplicationFactory _factory;

    protected ApiTest(IdentityModuleWebApplicationFactory factory)
    {
        _factory =  factory;
        var scope = _factory.Services.CreateScope();
        IdentityDbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        HttpClient = _factory.CreateClient();
    }

    private async Task InitializeDatabase()
    {
        var scope = _factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var testCustomer = new User
        {
            Email = PredefinedUserCredentials.Email,
            UserName = PredefinedUserCredentials.Email,
        };
        await userManager.CreateAsync(testCustomer, PredefinedUserCredentials.Password);
        await userManager.AddToRoleAsync(testCustomer, Role.Customer);
    }

    public async Task InitializeAsync() => await InitializeDatabase();

    public Task DisposeAsync() => Task.CompletedTask;
}