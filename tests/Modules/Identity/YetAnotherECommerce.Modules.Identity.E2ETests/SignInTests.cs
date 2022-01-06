using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Tests.Shared;

namespace YetAnotherECommerce.Modules.Identity.E2ETests
{
    public class SignInTests : IDisposable, IClassFixture<TestApplicationFactory>
    {
        private async Task<HttpResponseMessage> Act(SignInCommand command)
            => await _httpClient.PostAsync("identity-module/sign-in", JsonHelper.GetContent(command));

        [Fact]
        public async Task WithValidCredentials_ShouldReturnHttpStatusCodeOk()
        {
            var email = "test@yetanotherecommerce.com";
            var password = "super$ecret";
            var command = new SignInCommand(email, password);
            var user = new User(email, password, "customer");
            await _dbFixture.InsertAsync(user.AsDocument());

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task WithInvalidCredentials_ShouldReturnHttpStatusCodeBadRequest()
        {
            var email = "test@yetanotherecommerce.com";
            var password = "super$ecret";
            var command = new SignInCommand(email, "wrongPassword");
            var user = new User(email, password, "customer");
            await _dbFixture.InsertAsync(user.AsDocument());

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task WithNonExistingAccount_ShouldReturnHttpStatusCodeBadRequest()
        {
            var command = new SignInCommand("test@yetanotherecommerce.com", "wrongPassword");
            
            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        #region Arrange
        private readonly HttpClient _httpClient;
        private readonly MongoDbFixture<IdentityModuleMongoSettings, UserDocument> _dbFixture;

        public SignInTests(TestApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _dbFixture = new MongoDbFixture<IdentityModuleMongoSettings, UserDocument>("Users");
        }

        public void Dispose()
        {
            _dbFixture.Dispose();
        }
        #endregion
    }
}