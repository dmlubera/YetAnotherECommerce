using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using YetAnotherECommerce.Modules.Carts.Api.DI;
using YetAnotherECommerce.Modules.Identity.Api.DI;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Orders.Api.DI;
using YetAnotherECommerce.Modules.Orders.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Products.Api.DI;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Users.Api.DI;
using YetAnotherECommerce.Modules.Users.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Shared.Infrastructure.DI;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

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
            services.Configure<ProductsModuleMongoSettings>(Configuration.GetSection(nameof(ProductsModuleMongoSettings)));
            services.Configure<OrdersModuleMongoSettings>(Configuration.GetSection(nameof(OrdersModuleMongoSettings)));
            services.Configure<MessagingOptions>(Configuration.GetSection("Messaging"));
            services.AddIdentityModule();
            services.AddUsersModule();
            services.AddProductsModule();
            services.AddCartsModule();
            services.AddOrdersModule();
            services.AddInfrastructure(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseInfrastructure();

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