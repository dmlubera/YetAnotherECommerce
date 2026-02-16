using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Testcontainers.PostgreSql;
using Xunit;
using YetAnotherECommerce.Bootstrapper;
using YetAnotherECommerce.Modules.Identity.Core.DAL;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.IntegrationTests.Extensions;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests;

public class IdentityModuleWebApplicationFactory : WebApplicationFactory<Startup>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithCleanUp(true)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilCommandIsCompleted("pg_isready"))
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
        => builder
            .ConfigureAppConfiguration((_, config) =>
            {
                var dict = new Dictionary<string, string?>
                {
                    ["ConnectionStrings:Default"] = _dbContainer.GetConnectionString()
                };

                config.AddInMemoryCollection(dict);
            })
            .ConfigureTestServices(services =>
            {
                services.SetupDbContext<IdentityDbContext>(_dbContainer.GetConnectionString(), (context, _) =>
                {
                    var rolesToAdd = new List<IdentityRole<Guid>>
                    {
                        new()
                        {
                            Id = Guid.NewGuid(),
                            Name = Role.Admin,
                            NormalizedName = Role.Admin.ToUpperInvariant()
                        },
                        new()
                        {
                            Id = Guid.NewGuid(),
                            Name = Role.Customer,
                            NormalizedName = Role.Customer.ToUpperInvariant()
                        }
                    };
                    context.Set<IdentityRole<Guid>>().AddRange(rolesToAdd);
                    context.SaveChanges();
                });
            });

    public async Task InitializeAsync() => await _dbContainer.StartAsync();

    public new async Task DisposeAsync() => await _dbContainer.StopAsync();
}