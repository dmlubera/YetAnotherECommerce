using System;

namespace YetAnotherECommerce.Modules.Identity.Api.Endpoints.ConfirmEmail;

public record ConfirmEmailRequest(Guid UserId, string Token);