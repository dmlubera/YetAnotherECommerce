using Bogus;
using System;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignUp;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Fixtures.Commands
{
    public static class SignUpCommandFixture
    {
        public static SignUpCommand Create()
            => new Faker<SignUpCommand>()
                .CustomInstantiator(x => Activator.CreateInstance(typeof(SignUpCommand), nonPublic: true) as SignUpCommand)
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.Password, f => f.Internet.Password())
                .Generate();
    }
}