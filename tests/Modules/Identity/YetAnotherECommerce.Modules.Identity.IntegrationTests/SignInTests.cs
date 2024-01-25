using Bogus;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Api.Models.Requests;
using System.Net.Http.Json;
using Shouldly;
using YetAnotherECommerce.Shared.Abstractions.Auth;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests
{
    public class SignInTests : IntegrationTestBase
    {
        private readonly Faker _faker = new();

        public SignInTests(IdentityModuleWebApplicationFactory factory)
            : base(factory)
        {
        }

        private async Task<HttpResponseMessage> Act(SignInRequest request)
            => await HttpClient.PostAsJsonAsync("/identity-module/sign-in", request);

        [Fact]
        public async Task WhenCredntialsAreValid_ShouldReturnToken()
        {
            var request = new SignInRequest
            {
                Email = PredefinedUserCredentials.Email,
                Password = PredefinedUserCredentials.Password
            };

            var response = await Act(request);

            response.IsSuccessStatusCode.ShouldBeTrue();
            var token = await response.Content.ReadFromJsonAsync<JsonWebToken>();
            token.ShouldNotBeNull().AccessToken.ShouldNotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task WhenCredentialsAreInvalid_ShouldReturnBadRequest()
        {
            var request = new SignInRequest
            {
                Email = PredefinedUserCredentials.Email,
                Password = _faker.Internet.Password()
            };

            var response = await Act(request);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
