using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YetAnotherECommerce.Modules.Identity.Core.DI;
using YetAnotherECommerce.Modules.Identity.Core.Settings;
using YetAnotherECommerce.Shared.Abstractions.Modules;
using YetAnotherECommerce.Shared.Infrastructure.Auth;

namespace YetAnotherECommerce.Modules.Identity.Api;

internal class IdentityModule : IModule
{
    public const string BasePath = "identity-module";
    public string Name { get; } = "Identity";
    public string Path => BasePath;

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IdentityModuleSettings>(configuration.GetSection(nameof(IdentityModuleSettings)));
        var authSettings = new AuthSettings();
        configuration.GetSection(nameof(AuthSettings)).Bind(authSettings);
        services.AddSingleton(authSettings);

        services.AddCore(configuration);

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