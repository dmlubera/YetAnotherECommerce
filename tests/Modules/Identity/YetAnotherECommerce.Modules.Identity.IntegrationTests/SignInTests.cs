using Bogus;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Api.Models.Requests;
using System.Net.Http.Json;
using Shouldly;
using YetAnotherECommerce.Shared.Abstractions.Auth;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests;

public class SignInTests(IdentityModuleWebApplicationFactory factory) : IntegrationTestBase(factory)
{
    private readonly Faker _faker = new();

    private async Task<HttpResponseMessage> Act(SignInRequest request)
        => await HttpClient.PostAsJsonAsync("/identity-module/sign-in", request);

    [Fact]
    public async Task WhenCredentialsAreValid_ShouldReturnToken()
    {
        // Arrange
        var request = new SignInRequest
        {
            Email = PredefinedUserCredentials.Email,
            Password = PredefinedUserCredentials.Password
        };

        // Act
        var response = await Act(request);

        // Assert
        response.IsSuccessStatusCode.ShouldBeTrue();
        var token = await response.Content.ReadFromJsonAsync<JsonWebToken>();
        token.ShouldNotBeNull().AccessToken.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WhenCredentialsAreInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new SignInRequest
        {
            Email = PredefinedUserCredentials.Email,
            Password = _faker.Internet.Password()
        };

        // Act
        var response = await Act(request);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}