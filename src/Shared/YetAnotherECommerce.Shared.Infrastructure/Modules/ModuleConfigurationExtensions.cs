using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;

namespace YetAnotherECommerce.Shared.Infrastructure.Modules
{
    internal static class ModuleConfigurationExtensions
    {
        public static IHostBuilder ConfigureModules(this IHostBuilder builder)
            => builder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                foreach(var settings in GetSettings(ctx, "*"))
                {
                    cfg.AddJsonFile(settings);
                }

            });

        private static IEnumerable<string> GetSettings(HostBuilderContext ctx, string pattern)
            => Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, $"module.{pattern}.json", SearchOption.AllDirectories);
    }
}