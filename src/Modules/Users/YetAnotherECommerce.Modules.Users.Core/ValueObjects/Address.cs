using System;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;

namespace YetAnotherECommerce.Modules.Users.Core.ValueObjects
{
    public class Address : IEquatable<Address>
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string Country { get; private set; }

        public bool Equals(Address other)
        {
            return Street == other.Street
                && City == other.City
                && ZipCode == other.ZipCode
                && Country == other.Country;
        }

        private Address(string street, string city, string zipCode, string country)
        {
            Street = street;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }

        public static Address Create(string street, string city, string zipCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new InvalidStreetValueException();
            if (string.IsNullOrWhiteSpace(city))
                throw new InvalidCityValueException();
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new InvalidZipCodeValueException();
            if (string.IsNullOrWhiteSpace(country))
                throw new InvalidCountryValueException();

            return new Address(street, city, zipCode, country);
        }

        public override string ToString()
            => $"{Street}, {ZipCode} {City}, {Country}";
    }
}