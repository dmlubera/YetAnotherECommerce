using System;

namespace YetAnotherECommerce.Shared.Abstractions.Auth
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
    }
}