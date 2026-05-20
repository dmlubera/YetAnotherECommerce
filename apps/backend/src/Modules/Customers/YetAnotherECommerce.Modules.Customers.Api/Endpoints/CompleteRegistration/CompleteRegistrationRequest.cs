namespace YetAnotherECommerce.Modules.Customers.Api.Endpoints.CompleteRegistration;

public record CompleteRegistrationRequest(string FirstName, string LastName, string Street, string City, string ZipCode, string Country);