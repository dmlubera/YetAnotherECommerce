using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace YetAnotherECommerce.Shared.Infrastructure.Modules
{
    internal static class ModuleConfigurationExtensions
    {
        public static IHostBuilder ConfigureModules(this IHostBuilder builder)
            => builder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                foreach(var settings in GetSettings(ctx, "*", ctx.HostingEnvironment.EnvironmentName == "Test"))
                {
                    cfg.AddJsonFile(settings);
                }

            });

        private static IEnumerable<string> GetSettings(HostBuilderContext ctx, string pattern, bool isTestEvnironment)
        {
            var files = Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, $"module.{pattern}.json", SearchOption.AllDirectories);

            //TODO: Refactor this to work with any environment
            return isTestEvnironment ? files.Where(x => x.Contains(".Test.")) : files.Where(x => !x.Contains(".Test."));
        }
    }
}