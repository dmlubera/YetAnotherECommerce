using AutoFixture;
using Bogus;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Customizations;

public class SignUpCommandCustomizer : IFixtureCustomizer<SignUpCommand>
{
    public void Customize(IFixture fixture)
    {
        var faker = new Faker();
        fixture.Customize<SignUpCommand>(composer =>
            composer
                .With(command => command.Email, faker.Internet.Email())
                .With(command => command.Password, faker.Internet.Password()));
    }
}