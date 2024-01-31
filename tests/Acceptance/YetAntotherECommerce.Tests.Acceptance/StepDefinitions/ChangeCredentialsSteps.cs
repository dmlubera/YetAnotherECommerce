using Shouldly;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using YetAnotherECommerce.Tests.Acceptance.Models;
using YetAntotherECommerce.Tests.Acceptance;

namespace YetAnotherECommerce.Tests.Acceptance.StepDefinitions
{
    [Binding]
    internal class ChangeCredentialsSteps
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public ChangeCredentialsSteps(ScenarioContext scenarioContext, TestApplicationFactory factory)
        {
            _scenarioContext = scenarioContext;
            _httpClient = factory.CreateClient();
        }

        [Given(@"has successfully logged in")]
        public async Task HasSuccessfullyLogIn()
        {
            var (email, password) = _scenarioContext.Get<(string email, string password)>("Credentials");
            var request = new SignInRequest
            {
                Email = email,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("/identity-module/sign-in", request);

            response.IsSuccessStatusCode.ShouldBeTrue();

            var token = await response.Content.ReadFromJsonAsync<JsonWebToken>();
            _scenarioContext.Add("AuthToken", token?.AccessToken);
        }


        [When(@"changed email to '(.*)'")]
        public async Task WhenChangedEmailTo(string email)
        {
            var authToken = _scenarioContext.Get<string>("AuthToken");

            var request = new ChangeEmailRequest
            {
                Email = email
            };

            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + authToken);

            var response = await _httpClient.PostAsJsonAsync("/identity-module/account/change-email", request);

            response.IsSuccessStatusCode.ShouldBeTrue();

            _scenarioContext.Add("UpdatedEmail", email);
        }

        [Then(@"can sign in using new email")]
        public async Task ThenCanSignInUsingNewEmail()
        {
            var (_, password) = _scenarioContext.Get<(string email, string password)>("Credentials");
            var email = _scenarioContext.Get<string>("UpdatedEmail");

            var request = new SignInRequest
            {
                Email = email,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("/identity-module/sign-in", request);

            response.IsSuccessStatusCode.ShouldBeTrue();

            var token = await response.Content.ReadFromJsonAsync<JsonWebToken>();
            token.ShouldNotBeNull().AccessToken.ShouldNotBeNullOrWhiteSpace();
        }

        [When(@"changed password to '(.*)'")]
        public async Task WhenChangedPassowrdTo(string password)
        {
            var authToken = _scenarioContext.Get<string>("AuthToken");

            var request = new ChangePasswordRequest
            {
                Password = password
            };

            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + authToken);

            var response = await _httpClient.PostAsJsonAsync("/identity-module/account/change-password", request);

            response.IsSuccessStatusCode.ShouldBeTrue();

            _scenarioContext.Add("UpdatedPassword", password);
        }

        [Then(@"can sign in using new password")]
        public async Task ThenCanSignInUsingNewPassword()
        {
            var (email, _) = _scenarioContext.Get<(string email, string password)>("Credentials");
            var password = _scenarioContext.Get<string>("UpdatedPassword");

            var request = new SignInRequest
            {
                Email = email,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("/identity-module/sign-in", request);

            response.IsSuccessStatusCode.ShouldBeTrue();

            var token = await response.Content.ReadFromJsonAsync<JsonWebToken>();
            token.ShouldNotBeNull().AccessToken.ShouldNotBeNullOrWhiteSpace();
        }
    }
}
