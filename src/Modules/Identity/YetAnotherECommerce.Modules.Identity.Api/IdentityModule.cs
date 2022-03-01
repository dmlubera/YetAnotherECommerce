using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Text;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Identity.Core.DI;
using YetAnotherECommerce.Shared.Abstractions.Modules;
using YetAnotherECommerce.Shared.Infrastructure.Auth;

namespace YetAnotherECommerce.Modules.Identity.Api
{
    internal class IdentityModule : IModule
    {
        public const string BasePath = "identity-module";
        public string Name { get; } = "Identity";
        public string Path => BasePath;

        public void Register(IServiceCollection services)
        {
            services.AddTransient<IMongoClient>(sp =>
            {
                var mongoSettings = sp.GetRequiredService<IOptions<IdentityModuleMongoSettings>>().Value;
                return new MongoClient(mongoSettings.ConnectionString);
            });
            services.AddCore();

            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var authSettings = new AuthSettings();
            configuration.GetSection(nameof(AuthSettings)).Bind(authSettings);

            services.AddSingleton(authSettings);
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opts =>
            {
                opts.SaveToken = authSettings.SaveToken;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = authSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.IssuerSigningKey)),
                    ValidateIssuer = authSettings.ValidateIssuer,
                    ValidIssuer = authSettings.Issuer,
                    ValidateAudience = authSettings.ValidateAudience,
                    RequireExpirationTime = authSettings.RequireExpirationTime,
                    ValidateLifetime = authSettings.ValidateLifetime
                };
            });
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
