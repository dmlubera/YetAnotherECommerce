using AutoFixture;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Customizations;

internal interface IFixtureCustomizer<T> : ICustomization where T : class;
