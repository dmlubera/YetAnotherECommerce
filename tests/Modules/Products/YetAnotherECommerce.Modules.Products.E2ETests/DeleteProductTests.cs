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
    public class DeleteProductTests : IDisposable, IClassFixture<TestApplicationFactory>
    {
        private async Task<HttpResponseMessage> Act(DeleteProductCommand command)
            => await _httpClient.DeleteAsync($"products-module/products/{command.ProductId}");

        [Fact]
        public async Task WithoutAutentication_ShouldReturnHttpStatusCodeUnauthorized()
        {
            var command = new DeleteProductCommand(Guid.NewGuid());

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task WithAutenticationAsCustomer_ShouldReturnHttpStatusCodeForbidden()
        {
            var command = new DeleteProductCommand(Guid.NewGuid());
            var customer = await _identityDbFixture.GetAsync((UserDocument document) => document.Role == "customer");
            Authenticate(customer);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task WithAuthenticationAsAdminAndExistedProduct_ShouldReturnHttpStatusCodeNoContent()
        {
            var existedProduct = await _productsDbFixture.GetAsync((ProductDocument document) => document.Name == "Existed product");
            var admin = await _identityDbFixture.GetAsync((UserDocument document) => document.Role == "admin");
            var command = new DeleteProductCommand(existedProduct.Id);
            Authenticate(admin);

            var httpResponse = await Act(command);

            httpResponse.ShouldNotBeNull();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task WithAuthenticationAsAdminAndExistedProduct_ShouldRemoveProductFromDatabase()
        {
            var existedProduct = await _productsDbFixture.GetAsync((ProductDocument document) => document.Name == "Existed product");
            var admin = await _identityDbFixture.GetAsync((UserDocument document) => document.Role == "admin");
            var command = new DeleteProductCommand(existedProduct.Id);
            Authenticate(admin);

            await Act(command);

            var product = await _productsDbFixture.GetAsync(existedProduct.Id);
            product.ShouldBeNull();
        }

        #region
        private readonly HttpClient _httpClient;
        private readonly MongoDbFixture<IdentityModuleSettings, UserDocument> _identityDbFixture;
        private readonly MongoDbFixture<ProductsModuleSettings, ProductDocument> _productsDbFixture;

        public DeleteProductTests(TestApplicationFactory factory)
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
