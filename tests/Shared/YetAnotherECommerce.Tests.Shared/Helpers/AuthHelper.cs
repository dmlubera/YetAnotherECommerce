using System;
using YetAnotherECommerce.Shared.Infrastructure.Auth;

namespace YetAnotherECommerce.Tests.Shared.Helpers
{
    public static class AuthHelper
    {
        private static readonly AuthManager _authManager;

        static AuthHelper()
        {
            var options = OptionsHelper.GetOptions<AuthSettings>();
            _authManager = new AuthManager(options);
        }

        public static string GenerateJwt(Guid userId, string role)
            => _authManager.GenerateJwtToken(userId, role).AccessToken;
    }
}