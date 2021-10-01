using System;

namespace YetAnotherECommerce.Shared.Abstractions.Auth
{
    public interface IAuthManager
    {
        JsonWebToken GenerateJwtToken(Guid userId);
    }
}