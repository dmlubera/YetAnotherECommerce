using Microsoft.Extensions.Configuration;

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
            => GetConfigurationRoot().GetValue<string>("MongoSettings");

        private static IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
