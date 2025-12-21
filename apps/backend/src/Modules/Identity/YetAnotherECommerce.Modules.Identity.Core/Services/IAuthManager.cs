using System;
using YetAnotherECommerce.Modules.Identity.Core.Dtos;

namespace YetAnotherECommerce.Modules.Identity.Core.Services;

public interface IAuthManager
{
    JsonWebToken GenerateJwtToken(Guid userId, string userRole);
}