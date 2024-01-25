using Shouldly;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;
using YetAnotherECommerce.Tests.Acceptance.Models;
using YetAntotherECommerce.Tests.Acceptance.Models;

namespace YetAntotherECommerce.Tests.Acceptance.StepDefinitions
{
    [Binding]
    internal class AuthenticationSteps : IClassFixture<TestApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public AuthenticationSteps(ScenarioContext scenarioContext, TestApplicationFactory factory)
        {
            _scenarioContext = scenarioContext;
            _httpClient = factory.CreateClient();
        }

        [Given(@"customer has registered with credentials")]
        public async Task CustomerHasRegisteredWithCredentials(Table details)
        {
            var request = details.CreateInstance<SignUpRequest>();
            request.Role = "customer";

            var response = await _httpClient.PostAsJsonAsync("/identity-module/sign-up", request);

            _scenarioContext.Add("Credentials", (request.Email, request.Password));
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }


        [When(@"I sign up as customer with following credentials")]
        public async Task WhenISignUpAsCustomerWithFollowingDetails(Table details)
        {
            var request = details.CreateInstance<SignUpRequest>();
            request.Role = "customer";

            var response = await _httpClient.PostAsJsonAsync("/identity-module/sign-up", request);

            _scenarioContext.Add("Credentials", (request.Email, request.Password));
            _scenarioContext.Add("HttpResponse", response);
        }


        [When(@"I sign up as admin with following credentials")]
        public async Task WhenISignUpAsAdminWithFollowingDetails(Table details)
        {
            var request = details.CreateInstance<SignUpRequest>();
            request.Role = "admin";

            var response = await _httpClient.PostAsJsonAsync("/identity-module/sign-up", request);

            _scenarioContext.Add("Credentials", (request.Email, request.Password));
            _scenarioContext.Add("HttpResponse", response);
        }

        [When(@"trying to sign in with password '(.*)'")]
        public async Task WhenTryingToSignInWithIncorrectPassword(string password)
        {
            var email = _scenarioContext.Get<(string Email, string Password)>("Credentials").Email;
            var request = new SignInRequest
            {
                Email = email,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("/identity-module/sign-in", request);

            _scenarioContext.Add("HttpResponse", response);
        }

        [When(@"trying to sign up with email '(.*)'")]
        public async Task WhenTryingToSignUpWithAlreadyUsedEmail(string email)
        {
            var request = new SignUpRequest
            {
                Email = email,
                Password = "password",
                Role = "customer"
            };

            var response = await _httpClient.PostAsJsonAsync("/identity-module/sign-up", request);

            _scenarioContext.Add("HttpResponse", response);
        }

        [Then(@"customer account is successfully created")]
        public void ThenCustomerAccountIsSuccessfullyCreated()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("HttpResponse");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Then("it is possible to sign in successfully")]
        public async Task ThenItIsPossibleToSignInSuccessfully()
        {
            var (email, password) = _scenarioContext.Get<(string, string)>("Credentials");
            var request = new SignInRequest
            {
                Email = email,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("/identity-module/sign-in", request);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Then("will get error response")]
        public void ThenWillGetErrorResponse()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("HttpResponse");

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
