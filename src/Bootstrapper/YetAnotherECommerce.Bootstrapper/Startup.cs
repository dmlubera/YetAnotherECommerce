using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using YetAnotherECommerce.Modules.Identity.Api.DI;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Products.Api.DI;
using YetAnotherECommerce.Modules.Users.Api.DI;
using YetAnotherECommerce.Modules.Users.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Shared.Infrastructure.DI;

namespace YetAnotherECommerce.Bootstrapper
{
    public class Startup
    {
        public Startup(IConfiguration configuration) 
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IdentityModuleMongoSettings>(Configuration.GetSection(nameof(IdentityModuleMongoSettings)));
            services.Configure<UsersModuleMongoSettings>(Configuration.GetSection(nameof(UsersModuleMongoSettings)));
            services.AddInfrastructure(AppDomain.CurrentDomain.GetAssemblies());
            services.AddIdentityModule();
            services.AddUsersModule();
            services.AddProductsModule();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseInfrastructure();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", ctx => ctx.Response.WriteAsync("YetAnotherECommerce API!"));
            });
        }
    }
}