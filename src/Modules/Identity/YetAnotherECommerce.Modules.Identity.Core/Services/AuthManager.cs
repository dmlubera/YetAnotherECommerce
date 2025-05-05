using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using YetAnotherECommerce.Modules.Identity.Core.Dtos;
using AuthSettings = YetAnotherECommerce.Modules.Identity.Core.Settings.AuthSettings;

namespace YetAnotherECommerce.Modules.Identity.Core.Services;

public class AuthManager(AuthSettings authSettings) : IAuthManager
{
    public JsonWebToken GenerateJwtToken(Guid userId, string userRole)
    {
        var utcNow = DateTime.UtcNow;
        var expires = utcNow.Add(authSettings.Expiry);

        var claims = new List<Claim>()
        {
            new (JwtRegisteredClaimNames.Sub, userId.ToString()),
            new (JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Iat, new DateTimeOffset(utcNow).ToUnixTimeSeconds().ToString()),
            new (ClaimTypes.Role, userRole)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.IssuerSigningKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: authSettings.Issuer,
            claims: claims,
            notBefore: utcNow,
            expires: expires,
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        return new JsonWebToken(token, expires);
    }
}