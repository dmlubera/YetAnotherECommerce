using System;

namespace YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Documents
{
    public class UserDocument
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}