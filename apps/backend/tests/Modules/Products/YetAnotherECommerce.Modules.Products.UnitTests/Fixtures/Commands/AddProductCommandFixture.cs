using Bogus;
using YetAnotherECommerce.Modules.Products.Core.Commands;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Fixtures.Commands;

public static class AddProductCommandFixture
{
    public static AddProductCommand Create()
        => new Faker<AddProductCommand>()
            .CustomInstantiator(x => new AddProductCommand(
                Name: x.Commerce.ProductName(),
                Description: x.Commerce.ProductDescription(),
                Price: decimal.Parse(x.Commerce.Price()),
                Quantity: x.Random.Int(1, 100)))
            .Generate();
}