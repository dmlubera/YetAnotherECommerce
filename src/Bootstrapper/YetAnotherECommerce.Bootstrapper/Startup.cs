using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Users.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Shared.Abstractions.Modules;
using YetAnotherECommerce.Shared.Infrastructure.DI;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

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
            services.Configure<IdentityModuleMongoSettings>(Configuration.GetSection(nameof(IdentityModuleMongoSettings)));
            services.Configure<UsersModuleMongoSettings>(Configuration.GetSection(nameof(UsersModuleMongoSettings)));
            services.Configure<ProductsModuleMongoSettings>(Configuration.GetSection(nameof(ProductsModuleMongoSettings)));
            services.Configure<OrdersModuleMongoSettings>(Configuration.GetSection(nameof(OrdersModuleMongoSettings)));
            services.Configure<MessagingOptions>(Configuration.GetSection("Messaging"));

            foreach (var module in _modules)
            {
                module.Register(services);
            }
            
            services.AddInfrastructure(AppDomain.CurrentDomain.GetAssemblies());

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