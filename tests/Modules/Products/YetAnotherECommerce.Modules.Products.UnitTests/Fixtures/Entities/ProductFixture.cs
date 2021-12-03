using Bogus;
using System;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.ValueObjects;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Fixtures.Entities
{
    public static class ProductFixture
    {
        public static Product Create()
            => new Faker<Product>()
                .CustomInstantiator(x => Activator.CreateInstance(typeof(Product), nonPublic: true) as Product)
                .RuleFor(x => x.Quantity, f => Quantity.Create(10))
                .RuleFor(x => x.Price, f => Price.Create(10))
                .Generate();
    }
}