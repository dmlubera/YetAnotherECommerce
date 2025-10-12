using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Identity.Core.Settings;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Documents;
using YetAnotherECommerce.Modules.Products.Core.Settings;
using YetAnotherECommerce.Tests.Shared;
using YetAnotherECommerce.Tests.Shared.Helpers;
using YetAnotherECommerce.Tests.Shared.Initializers;

namespace YetAnotherECommerce.Modules.Products.E2ETests
{
    public class AddProductToCartTests : IDisposable, IClassFixture<TestApplicationFactory>
    {
        private async Task<HttpResponseMessage> Act(AddProductToCartCommand command)
            => await _httpClient.PostAsync("products-module/products/add-to-cart", JsonHelper.GetContent(command));

        [Fact]
        public async Task WithoutAuthentication_ShouldReturnHttpStatusCodeUnauthorized()
        {
            var command = new AddProductToCartCommand(
                customerId: Guid.NewGuid(),
                productId: Guid.NewGuid(),
                quantity: 5);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task WithAuthenticationAsAdmin_ShouldReturnHttpStatusCodeForbidden()
        {
            var command = new AddProductToCartCommand(
                customerId: Guid.NewGuid(),
                productId: Guid.NewGuid(),
                quantity: 5);
            var admin = await _identityDbFixture.GetAsync((UserDocument document) => document.Role == "admin");
            Authenticate(admin);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task WithQuantityGreaterThanAvailable_ShouldReturnHttpStatusCodeBadRequest()
        {
            var command = new AddProductToCartCommand(
                customerId: Guid.NewGuid(),
                productId: Guid.NewGuid(),
                quantity: 999);
            var customer = await _identityDbFixture.GetAsync((UserDocument document) => document.Role == "customer");
            Authenticate(customer);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task WithAuthenticationAsCustomerAndWithNotExistedProduct_ShouldReturnHttpStatusCodeBadRequest()
        {
            var command = new AddProductToCartCommand(
                customerId: Guid.NewGuid(),
                productId: Guid.NewGuid(),
                quantity: 5);
            var customer = await _identityDbFixture.GetAsync((UserDocument document) => document.Role == "customer");
            Authenticate(customer);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task WithAuthenticationAsCustomerAndWithExistedProduct_ShouldReturnHttpStatusCodeOk()
        {
            var existedProduct = await _productsDbFixture.GetAsync((ProductDocument document) => document.Name == "Existed product");
            var command = new AddProductToCartCommand(
                customerId: Guid.NewGuid(),
                productId: existedProduct.Id,
                quantity: 5);
            var customer = await _identityDbFixture.GetAsync((UserDocument document) => document.Role == "customer");
            Authenticate(customer);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        #region Arrange
        private readonly HttpClient _httpClient;
        private readonly MongoDbFixture<IdentityModuleSettings, UserDocument> _identityDbFixture;
        private readonly MongoDbFixture<ProductsModuleSettings, ProductDocument> _productsDbFixture;

        public AddProductToCartTests(TestApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _identityDbFixture = new MongoDbFixture<IdentityModuleSettings, UserDocument>("Users");
            _productsDbFixture = new MongoDbFixture<ProductsModuleSettings, ProductDocument>("Products");
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