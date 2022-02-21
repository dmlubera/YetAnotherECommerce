using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Settings;
using YetAnotherECommerce.Tests.Shared;
using YetAnotherECommerce.Tests.Shared.Helpers;
using YetAnotherECommerce.Tests.Shared.Initializers;

namespace YetAnotherECommerce.Modules.Products.E2ETests
{
    public class UpdateQuantityTests : IDisposable, IClassFixture<TestApplicationFactory>
    {
        private async Task<HttpResponseMessage> Act(UpdateQuantityCommand command)
            => await _httpClient.PostAsync($"products-module/products/update-quantity", JsonHelper.GetContent(command));

        [Fact]
        public async Task WithoutAuthentication_ShouldReturnHttpStatusCodeUnauthorized()
        {
            var command = new UpdateQuantityCommand(
                productId: Guid.NewGuid(),
                quantity: 100);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task WithAuthenitcationAsCustomer_ShouldReturnHttpStatusCodeForbidden()
        {
            var command = new UpdateQuantityCommand(
                productId: Guid.NewGuid(),
                quantity: 100);
            var customer = await _identityDbFixture.GetAsync((UserDocument document) => document.Role == "customer");
            Authenticate(customer);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task WithAuthenicationAsAdminAndNonExistedProduct_ShouldReturnHttpStatusCodeBadRequest()
        {
            var command = new UpdateQuantityCommand(
                productId: Guid.NewGuid(),
                quantity: 100);
            var customer = await _identityDbFixture.GetAsync((UserDocument document) => document.Role == "admin");
            Authenticate(customer);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task WithAuthenticationAsAdminAndExistedProduct_ShouldReturnHttpStatusCodeOk()
        {
            var existedProduct = await _productsDbFixture.GetAsync((ProductDocument document) => document.Name == "Existed product");
            var command = new UpdateQuantityCommand(
                productId: existedProduct.Id,
                quantity: 100);
            var customer = await _identityDbFixture.GetAsync((UserDocument document) => document.Role == "admin");
            Authenticate(customer);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task WithAuthenticationAsAdminAndExistedProduct_ShouldUpdateProductInDatabase()
        {
            var existedProduct = await _productsDbFixture.GetAsync((ProductDocument document) => document.Name == "Existed product");
            var command = new UpdateQuantityCommand(
                productId: existedProduct.Id,
                quantity: 100);
            var customer = await _identityDbFixture.GetAsync((UserDocument document) => document.Role == "admin");
            Authenticate(customer);

            await Act(command);

            var updatedProduct = await _productsDbFixture.GetAsync(existedProduct.Id);
            updatedProduct.ShouldNotBeNull();
            updatedProduct.Quantity.ShouldBe(command.Quantity);
        }

        #region Arrange
        private readonly HttpClient _httpClient;
        private readonly MongoDbFixture<IdentityModuleMongoSettings, UserDocument> _identityDbFixture;
        private readonly MongoDbFixture<ProductsModuleMongoSettings, ProductDocument> _productsDbFixture;

        public UpdateQuantityTests(TestApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _identityDbFixture = new MongoDbFixture<IdentityModuleMongoSettings, UserDocument>("Users");
            _productsDbFixture = new MongoDbFixture<ProductsModuleMongoSettings, ProductDocument>("Products");
            _identityDbFixture.InitializeAsync(new IdentityDbSeeder());
            _productsDbFixture.InitializeAsync(new ProductsDbSeeder());
        }

        private void Authenticate(UserDocument user)
        {
            var jwt = AuthHelper.GenerateJwt(user.Id, user.Role);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        }

        public void Dispose()
        {
            _identityDbFixture.Dispose();
            _productsDbFixture.Dispose();
        }
        #endregion
    }
}