using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;
using YetAnotherECommerce.Shared.Abstractions.Modules;
using YetAnotherECommerce.Shared.Infrastructure.DI;

namespace YetAnotherECommerce.Bootstrapper
{
    public class Startup
    {
        private readonly IList<Assembly> _assemblies;
        private readonly IList<IModule> _modules;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _assemblies = ModuleLoader.LoadAssemblies();
            _modules = ModuleLoader.LoadModules(_assemblies);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            foreach (var module in _modules)
            {
                module.Register(services, Configuration);
            }
            
            services.AddInfrastructure(AppDomain.CurrentDomain.GetAssemblies(), Configuration);

            services.AddHttpContextAccessor();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseInfrastructure();

            foreach(var module in _modules)
            {
                module.Use(app);
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", ctx => ctx.Response.WriteAsync("YetAnotherECommerce API!"));
            });
        }
    }
}