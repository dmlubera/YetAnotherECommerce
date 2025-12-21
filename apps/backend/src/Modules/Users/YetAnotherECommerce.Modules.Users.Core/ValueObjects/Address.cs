using System.Collections.Generic;
using YetAnotherECommerce.Modules.Users.Core.Exceptions;
using YetAnotherECommerce.Shared.Abstractions.BuildingBlocks;

namespace YetAnotherECommerce.Modules.Users.Core.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string ZipCode { get; private set; }
    public string Country { get; private set; }

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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return ZipCode;
        yield return Country;
    }
}