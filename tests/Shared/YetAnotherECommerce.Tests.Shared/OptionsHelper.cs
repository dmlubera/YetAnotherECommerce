using Microsoft.Extensions.Configuration;
using YetAnotherECommerce.Shared.Abstractions.Mongo;

namespace YetAnotherECommerce.Tests.Shared
{
    public static class OptionsHelper
    {
        public static TOptions GetOptions<TOptions>() where TOptions : IMongoSettings, new()
        {
            var options = new TOptions();

            GetConfigurationRoot()
                .GetSection(typeof(TOptions).Name).Bind(options);

            return options;
        }

        private static IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
