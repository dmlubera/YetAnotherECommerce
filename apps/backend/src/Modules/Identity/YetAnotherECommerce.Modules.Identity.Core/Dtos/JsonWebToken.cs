using System;

namespace YetAnotherECommerce.Modules.Identity.Core.Dtos;

public record JsonWebToken(string AccessToken, DateTime Expires);