using System;

namespace YetAnotherECommerce.Modules.Identity.Api.Endpoints.ResetPassword;

public record ResetPasswordRequest(Guid UserId, string Token, string Password);