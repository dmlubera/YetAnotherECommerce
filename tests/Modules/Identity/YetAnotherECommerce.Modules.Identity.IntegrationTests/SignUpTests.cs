using Bogus;
using Shouldly;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Identity.Api.Models.Requests;
using YetAnotherECommerce.Modules.Identity.Core.Entities;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests
{
    public class SignUpTests : IntegrationTestBase
    {
        private readonly Faker _faker = new();

        public SignUpTests(IdentityModuleWebApplicationFactory factory)
            : base(factory)
        {
        }

        private async Task<HttpResponseMessage> Act(SignUpRequest request)
            => await HttpClient.PostAsJsonAsync("/identity-module/sign-up", request);

        [Fact]
        public async Task WhenRequestIsValid_ShouldCreateUser()
        {
            var request = new SignUpRequest
            {
                Email = _faker.Internet.Email(),
                Password = _faker.Internet.Password(),
                Role = "customer"
            };

            var response = await Act(request);

            response.IsSuccessStatusCode.ShouldBeTrue();
            IdentityDbContext.Users.Where(x => x.Email == request.Email).ToList().ShouldHaveSingleItem();
        }

        [Fact]
        public async Task WhenEmailAlreadyExists_ShouldReturnBadRequest()
        {
            var user = new User(Guid.NewGuid(), _faker.Internet.Email(), string.Empty, string.Empty, "Customer", DateTime.UtcNow, DateTime.UtcNow);
            IdentityDbContext.Users.Add(user);
            await IdentityDbContext.SaveChangesAsync();

            var request = new SignUpRequest
            {
                Email = user.Email,
                Password = "super$ecret",
                Role = "customer"
            };

            var response = await Act(request);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            IdentityDbContext.Users.Where(x => x.Email == request.Email).ToList().ShouldHaveSingleItem();
        }
    }
}