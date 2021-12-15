using System;
using YetAnotherECommerce.Modules.Users.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Users.Core.DAL.Mongo.Documents
{
    public class UserDocument
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }
        public bool IsRegistrationCompleted { get; set; }
    }
}