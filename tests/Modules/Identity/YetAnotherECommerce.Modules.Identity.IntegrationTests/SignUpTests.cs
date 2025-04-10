using Bogus;
using Shouldly;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Api.Models.Requests;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests;

public class SignUpTests(IdentityModuleWebApplicationFactory factory) : IntegrationTestBase(factory)
{
    private readonly Faker _faker = new();

    private async Task<HttpResponseMessage> Act(SignUpRequest request)
        => await HttpClient.PostAsJsonAsync("/identity-module/sign-up", request);

    [Fact]
    public async Task WhenRequestIsValid_ShouldCreateUser()
    {
        // Arrange
        var request = new SignUpRequest
        {
            Email = _faker.Internet.Email(),
            Password = _faker.Internet.Password(),
            Role = "customer"
        };

        // Act
        var response = await Act(request);
            
        // Assert
        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Fact]
    public async Task WhenEmailAlreadyExists_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new SignUpRequest
        {
            Email = PredefinedUserCredentials.Email,
            Password = "super$ecret",
            Role = "customer"
        };

        // Act
        var response = await Act(request);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        IdentityDbContext.Users.Where(x => x.Email == request.Email).ToList().ShouldHaveSingleItem();
    }
}