using System.Linq;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.DomainEvents;
using YetAnotherECommerce.Modules.Products.Core.Entitites;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Entities;

public class ProductTests
{
    [Fact]
    public void UpdateQuantity_ShouldUpdateAndAddDomainEvent()
    {
        var product = Mock.Of<Product>();
        var quantity = 10;

        product.UpdateQuantity(quantity);

        product.Quantity.Value.ShouldBe(quantity);
        product.Events.First().ShouldBeOfType<QuantityUpdated>();
    }

    [Fact]
    public void UpdatePrice_ShouldUpdateAndAddDomainEvent()
    {
        var product = Mock.Of<Product>();
        var price = 10;

        product.UpdatePrice(price);

        product.Price.Value.ShouldBe(price);
        product.Events.First().ShouldBeOfType<PriceUpdated>();
    }
}