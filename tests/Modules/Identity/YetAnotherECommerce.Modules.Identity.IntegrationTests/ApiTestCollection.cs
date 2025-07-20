using Xunit;

namespace YetAnotherECommerce.Modules.Identity.IntegrationTests;

[CollectionDefinition(nameof(ApiTestCollection))]
public class ApiTestCollection : ICollectionFixture<IdentityModuleWebApplicationFactory>;