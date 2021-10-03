using Bogus;
using System;
using YetAnotherECommerce.Modules.Identity.Core.Commands.SignIn;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Fixtures.Commands
{
    public static class SignInCommandFixture
    {
        public static SignInCommand Create()
            => new Faker<SignInCommand>()
                .CustomInstantiator(x => Activator.CreateInstance(typeof(SignInCommand), nonPublic: true) as SignInCommand)
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.Password, f => f.Internet.Password())
                .Generate();
    }
}