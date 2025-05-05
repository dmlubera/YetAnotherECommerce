using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bogus;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Api.Endpoints.SignIn;
using YetAnotherECommerce.Modules.Identity.Core.Dtos;

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
        var request = new SignInRequest(PredefinedUserCredentials.Email, PredefinedUserCredentials.Password);

        // Act
        var response = await Act(request);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var token = await response.Content.ReadFromJsonAsync<JsonWebToken>();
        token.ShouldNotBeNull().AccessToken.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WhenCredentialsAreInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new SignInRequest(PredefinedUserCredentials.Email, _faker.Internet.Password());
        
        // Act
        var response = await Act(request);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}