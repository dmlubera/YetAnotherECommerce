﻿namespace YetAnotherECommerce.Modules.Identity.Api.Models.Requests
{
    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}