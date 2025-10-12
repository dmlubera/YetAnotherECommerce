using System;

namespace YetAnotherECommerce.Modules.Orders.Core.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Customer(Guid id, string firstName, string lastName, string email, string address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
        }
    }
}