using System;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace YetAnotherECommerce.Bootstrapper;

public class Program
{
    public static Task Main(string[] args)
        => CreateHostBuilder(args).Build().RunAsync();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, configuration) =>
            {
                var currentConfig = configuration.Build();
                var kvName = currentConfig["KeyVaultSettings:Name"];
                if (!string.IsNullOrWhiteSpace(kvName))
                {
                    var kvUri = new Uri($"https://{kvName}.vault.azure.net/");
                    configuration.AddAzureKeyVault(kvUri, new DefaultAzureCredential());
                }
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog((context, _, configuration) =>
            {
                configuration.Enrich.FromLogContext()
                    .WriteTo.Console(outputTemplate:
                        "[{Timestamp:HH:mm:ss} {Level:u3} CorrelationId: {CorrelationId}] {Message:lj}{NewLine}{Exception}")
                    .ReadFrom.Configuration(context.Configuration);
            });
}