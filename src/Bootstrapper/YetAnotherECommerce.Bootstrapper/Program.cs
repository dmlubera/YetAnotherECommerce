using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Infrastructure.Modules;

namespace YetAnotherECommerce.Bootstrapper
{
    public class Program
    {
        public static Task Main(string[] args)
            => CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    var keyVaultUrl = configuration.Build()["KeyVaultSettings:Url"];

                    var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
                    configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureModules()
                .UseSerilog((context, services, configuration) =>
                {
                    configuration.Enrich.FromLogContext()
                        .WriteTo.Console(outputTemplate:
                        "[{Timestamp:HH:mm:ss} {Level:u3} CorrelationId: {CorrelationId}] {Message:lj}{NewLine}{Exception}")
                        .WriteTo.Seq(context.Configuration.GetSection("Seq:Url").Value)
                        .WriteTo.ApplicationInsights(
                            services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces)
                        .ReadFrom.Configuration(context.Configuration);
                });
    }
}