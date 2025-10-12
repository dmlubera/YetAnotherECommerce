using Bogus;
using System;
using YetAnotherECommerce.Modules.Products.Core.Commands;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Fixtures.Commands
{
    public static class AddProductCommandFixture
    {
        public static AddProductCommand Create()
            => new Faker<AddProductCommand>()
                .CustomInstantiator(x => Activator.CreateInstance(typeof(AddProductCommand), nonPublic: true) as AddProductCommand)
                .RuleFor(x => x.Name, f => f.Commerce.ProductName())
                .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                .RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
                .Generate();
    }
}