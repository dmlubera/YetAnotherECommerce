using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Identity.Core.Entities;
using YetAnotherECommerce.Modules.Identity.Core.Helpers;
using YetAnotherECommerce.Modules.Identity.Core.Settings;
using YetAnotherECommerce.Modules.Identity.Core.ValueObjects;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Products.Core.Settings;
using YetAnotherECommerce.Tests.Shared;
using YetAnotherECommerce.Tests.Shared.Helpers;

namespace YetAnotherECommerce.Modules.Products.E2ETests
{
    public class AddProductTests : IDisposable, IClassFixture<TestApplicationFactory>
    {
        private async Task<HttpResponseMessage> Act(AddProductCommand command)
            => await _httpClient.PostAsync("products-module/products/", JsonHelper.GetContent(command));

        [Fact]
        public async Task WithoutAuthentication_ShouldReturnHttpStatusCodeUnauthorized()
        {
            var command = new AddProductCommand(
                name: "Ultrabook",
                description: "High performance ultrabook",
                price: 100,
                quantity: 10);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task WithAuthenticationAsCustomer_ShouldReturnHttpStatusCodeForbidden()
        {
            var command = new AddProductCommand(
                name: "Ultrabook",
                description: "High performance ultrabook",
                price: 100,
                quantity: 10);
            var user = await AddIdentityAsync("customer");
            GenerateAuthenticationHeader(user);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task WithAuthenticationAsAdminAndValidData_ShouldReturnHttpStatusCodeOk()
        {
            var command = new AddProductCommand(
                name: "Ultrabook",
                description: "High performance ultrabook",
                price: 100,
                quantity: 10);
            var user = await AddIdentityAsync("admin");
            GenerateAuthenticationHeader(user);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task WithAuthenticationAsAdminAndValidData_ShouldAddProductToDatabase()
        {
            var command = new AddProductCommand(
                name: "Ultrabook",
                description: "High performance ultrabook",
                price: 100,
                quantity: 10);
            var user = await AddIdentityAsync("admin");
            GenerateAuthenticationHeader(user);

            await Act(command);

            var product = await _productsDbFixture.GetAsync((ProductDocument product) => product.Name == command.Name);
            product.ShouldNotBeNull();
            product.Name.ShouldBe(command.Name);
            product.Description.ShouldBe(command.Description);
            product.Price.ShouldBe(command.Price);
            product.Quantity.ShouldBe(command.Quantity);
        }

        #region Arrange
        private readonly HttpClient _httpClient;
        private readonly MongoDbFixture<ProductsModuleSettings, ProductDocument> _productsDbFixture;
        private readonly MongoDbFixture<IdentityModuleSettings, UserDocument> _identityDbFixture;

        public AddProductTests(TestApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _productsDbFixture = new MongoDbFixture<ProductsModuleSettings, ProductDocument>("Products");
            _identityDbFixture = new MongoDbFixture<IdentityModuleSettings, UserDocument>("Users");
        }

        private async Task<UserDocument> AddIdentityAsync(string role)
        {
            var customer = CreateUser(
                email: "test@yetanotherecommerce.com",
                password: "super$ecret",
                role: role);

            await _identityDbFixture.InsertAsync(customer.AsDocument());
            return await _identityDbFixture.GetAsync((UserDocument user) => user.Role == role);
        }

        private void GenerateAuthenticationHeader(UserDocument user)
        {
            var jwt = AuthHelper.GenerateJwt(user.Id, user.Role);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        }

        private static User CreateUser(string email, string password, string role)
        {
            var encrypter = new Encrypter();
            var salt = encrypter.GetSalt();
            var hash = encrypter.GetHash(password, salt);

            return User.Create(Email.Create(email), Password.Create(hash, salt), role);
        }

        public void Dispose()
        {
            _productsDbFixture.Dispose();
            _identityDbFixture.Dispose();
        }
        #endregion
    }
}