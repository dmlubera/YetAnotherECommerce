using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Identity.Core.DI;
using YetAnotherECommerce.Shared.Infrastructure.Auth;

[assembly: InternalsVisibleTo("YetAnotherECommerce.Bootstrapper")]
namespace YetAnotherECommerce.Modules.Identity.Api.DI
{
    internal static class IdentityModuleInstaller
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services)
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

            return services;
        }
    }
}