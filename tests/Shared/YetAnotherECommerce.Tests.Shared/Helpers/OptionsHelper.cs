using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace YetAnotherECommerce.Tests.Shared.Helpers
{
    public static class OptionsHelper
    {
        public static TOptions GetOptions<TOptions>() where TOptions : new()
        {
            var options = new TOptions();

            GetConfigurationRoot()
                .GetSection(typeof(TOptions).Name).Bind(options);

            return options;
        }

        public static string GetConnectionString()
            => GetConfigurationRoot().GetValue<string>("MongoDbSettings:ConnectionString");

        private static IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json", optional: true)
                .AddModulesConfigurations()
                .AddEnvironmentVariables()
                .Build();
        }

        private static IConfigurationBuilder AddModulesConfigurations(this IConfigurationBuilder builder)
        {
            Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "module.*.Test.json", SearchOption.AllDirectories).ToList().ForEach(x => builder.AddJsonFile(x, optional: true));

            return builder;
        }
    }
}
