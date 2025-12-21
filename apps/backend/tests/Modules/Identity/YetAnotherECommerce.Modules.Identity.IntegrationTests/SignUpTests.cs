using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bogus;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Api.Endpoints.SignUp;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests;

public class SignUpTests(IdentityModuleWebApplicationFactory factory) : ApiTest(factory)
{
    private readonly Faker _faker = new();

    private async Task<HttpResponseMessage> Act(SignUpRequest request)
        => await HttpClient.PostAsJsonAsync("/identity-module/sign-up", request);

    [Fact]
    public async Task WhenRequestIsValid_ShouldCreateUser()
    {
        // Arrange
        var request = new SignUpRequest(_faker.Internet.Email(), _faker.Internet.Password(prefix: "$"));

        // Act
        var response = await Act(request);
            
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task WhenEmailAlreadyExists_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new SignUpRequest(PredefinedUserCredentials.Email, _faker.Internet.Password(prefix: "$"));

        // Act
        var response = await Act(request);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        IdentityDbContext.Users.Where(x => x.Email == request.Email).ToList().ShouldHaveSingleItem();
    }
}