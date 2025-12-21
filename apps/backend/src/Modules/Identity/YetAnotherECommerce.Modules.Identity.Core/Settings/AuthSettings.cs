using System;

namespace YetAnotherECommerce.Modules.Identity.Core.Settings;

public class AuthSettings
{
    public bool SaveToken { get; set; } = true;
    public bool ValidateIssuer { get; set; } = true;
    public string Issuer { get; set; }
    public bool ValidateIssuerSigningKey { get; set; } = true;
    public string IssuerSigningKey { get; set; }
    public bool RequireExpirationTime { get; set; } = true;
    public TimeSpan Expiry { get; set; }
    public bool ValidateAudience { get; set; } = false;
    public bool ValidateLifetime { get; set; } = true;
}