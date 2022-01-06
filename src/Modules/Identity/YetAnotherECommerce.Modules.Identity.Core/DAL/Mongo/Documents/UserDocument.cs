using System;
using YetAnotherECommerce.Shared.Abstractions.Mongo;

namespace YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Documents
{
    public class UserDocument : IDocument
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}