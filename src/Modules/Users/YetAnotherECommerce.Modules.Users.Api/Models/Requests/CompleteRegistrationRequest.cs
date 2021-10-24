namespace YetAnotherECommerce.Modules.Users.Api.Models.Requests
{
    public class CompleteRegistrationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public CompleteRegistrationRequest(string firstName, string lastName,
            string street, string city, string zipCode, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }
    }
}