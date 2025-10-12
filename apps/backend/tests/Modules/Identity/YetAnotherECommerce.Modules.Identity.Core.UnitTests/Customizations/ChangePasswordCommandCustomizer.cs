using AutoFixture;
using Bogus;
using YetAnotherECommerce.Modules.Identity.Core.Commands.ChangePassword;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Customizations;

public class ChangePasswordCommandCustomizer : IFixtureCustomizer<ChangePasswordCommand>
{
    public void Customize(IFixture fixture)
    {
        var faker = new Faker();
        fixture.Customize<ChangePasswordCommand>(composer =>
            composer
                .With(command => command.NewPassword, faker.Internet.Password())
                .With(command => command.NewPassword, faker.Internet.Password()));
    }
}