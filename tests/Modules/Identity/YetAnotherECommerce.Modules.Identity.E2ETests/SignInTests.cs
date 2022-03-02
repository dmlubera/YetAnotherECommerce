using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Helpers;
using YetAnotherECommerce.Modules.Identity.Core.Settings;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;
using YetAnotherECommerce.Tests.Shared;
using YetAnotherECommerce.Tests.Shared.Helpers;

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
            var user = CreateUser(email, password, "customer");
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
            var user = CreateUser(email, password, "customer");
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

        private static User CreateUser(string email, string password, string role)
        {
            var encrypter = new Encrypter();
            var salt = encrypter.GetSalt();
            var hash = encrypter.GetHash(password, salt);

            return User.Create(Email.Create(email), Password.Create(hash, salt), role);
        }

        #region Arrange
        private readonly HttpClient _httpClient;
        private readonly MongoDbFixture<IdentityModuleSettings, UserDocument> _dbFixture;

        public SignInTests(TestApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _dbFixture = new MongoDbFixture<IdentityModuleSettings, UserDocument>("Users");
        }

        public void Dispose()
        {
            _dbFixture.Dispose();
        }
        #endregion
    }
}