using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Carts.Core.Entities;
using YetAnotherECommerce.Modules.Carts.Core.Events.External.Handlers;
using YetAnotherECommerce.Modules.Carts.Core.Events.External.Models;
using YetAnotherECommerce.Shared.Abstractions.Cache;

namespace YetAnotherECommerce.Modules.Carts.UnitTests.Events;

public class ProductAddedToCartHandlerTests
{
    private readonly Mock<ICache> _cacheMock;
    private readonly ProductAddedToCartHandler _handler;

    public ProductAddedToCartHandlerTests()
    {
        _cacheMock = new Mock<ICache>();
        _handler = new ProductAddedToCartHandler(_cacheMock.Object);
    }

    [Fact]
    public async Task WhenCartExist_ThenShouldAddItem()
    {
        var @event = new ProductAddedToCart(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "High performance ultrabook",
            10,
            10);
        var cart = new Cart();
        _cacheMock
            .Setup(x => x.Get(It.IsAny<ICacheKey<Cart>>()))
            .Returns(cart);

        await _handler.HandleAsync(@event);

        cart.Items.ShouldHaveSingleItem();
        var cartItem = cart.Items.FirstOrDefault();
        cartItem.ProductId.ShouldBe(@event.ProductId);
        cartItem.Name.ShouldBe(@event.Name);
        cartItem.UnitPrice.ShouldBe(@event.UnitPrice);
        cartItem.Quantity.ShouldBe(@event.Quantity);
    }
}