using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YetAnotherECommerce.Shared.Abstractions.Auth;

namespace YetAnotherECommerce.Shared.Infrastructure.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly AuthSettings _authSettings;

        public AuthManager(AuthSettings authSettings)
        {
            _authSettings = authSettings;
        }

        public JsonWebToken GenerateJwtToken(Guid userId)
        {
            var utcNow = DateTime.UtcNow;
            var expires = utcNow.Add(_authSettings.Expiry);

            var claims = new List<Claim>()
            {
                new (JwtRegisteredClaimNames.Sub, userId.ToString()),
                new (JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Iat, new DateTimeOffset(utcNow).ToUnixTimeSeconds().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.IssuerSigningKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _authSettings.Issuer,
                claims: claims,
                notBefore: utcNow,
                expires: expires,
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                Expires = expires
            };
        }
    }
}