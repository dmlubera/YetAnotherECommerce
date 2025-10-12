using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Identity.Core.Settings;
using YetAnotherECommerce.Tests.Shared;
using YetAnotherECommerce.Tests.Shared.Helpers;

namespace YetAnotherECommerce.Modules.Identity.E2ETests
{
    public class SignUpTests : IDisposable, IClassFixture<TestApplicationFactory>
    {
        private async Task<HttpResponseMessage> Act(SignUpCommand command)
            => await _httpClient.PostAsync("/identity-module/sign-up", JsonHelper.GetContent(command));

        [Fact]
        public async Task WithCorrectData_ShouldReturnHttpStatusCodeOk()
        {
            var command = new SignUpCommand(
                email: "test@yetanotherecommerce.com",
                password: "super$ecret",
                role: "customer");

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task WithCorrectData_ShouldAddUserToDatabase()
        {
            var command = new SignUpCommand(
                email: "test@yetanotherecommerce.com",
                password: "super$ecret",
                role: "customer");

            var httpResponse = await Act(command);

            var document = await _dbFixture.GetAsync((UserDocument userDocument) => userDocument.Email == command.Email);
            document.ShouldNotBeNull();
        }

        [Fact]
        public async Task WithExistingEmail_ShoudlReturnHttpStatusCodeBadRequest()
        {
            var command = new SignUpCommand(
                email: "test@yetanotherecommerce.com",
                password: "super$ecret",
                role: "customer");
            await _dbFixture.InsertAsync(new UserDocument
            {
                Id = Guid.NewGuid(),
                Email = command.Email
            });

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        #region Arrange
        private readonly HttpClient _httpClient;
        private readonly MongoDbFixture<IdentityModuleSettings, UserDocument> _dbFixture;
        
        public SignUpTests(TestApplicationFactory factory)
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