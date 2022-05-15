using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using YetAnotherECommerce.Bootstrapper;

namespace YetAnotherECommerce.Tests.Shared
{
    public class TestApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
            => builder.UseEnvironment("Test");
    }
}