using AutoFixture;
using Bogus;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Customizations;

public class SignInCommandCustomizer : IFixtureCustomizer<SignInCommand>
{
    public void Customize(IFixture fixture)
    {
        var faker = new Faker();
        fixture.Customize<SignInCommand>(composer =>
            composer
                .With(command => command.Email, faker.Internet.Email())
                .With(command => command.Password, faker.Internet.Password()));
    }
}