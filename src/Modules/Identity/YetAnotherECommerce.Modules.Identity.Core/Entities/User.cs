using System;
using Microsoft.AspNetCore.Identity;

namespace YetAnotherECommerce.Modules.Identity.Core.Entities;

public class User : IdentityUser<Guid>
{
    public string Role { get; set; }
}