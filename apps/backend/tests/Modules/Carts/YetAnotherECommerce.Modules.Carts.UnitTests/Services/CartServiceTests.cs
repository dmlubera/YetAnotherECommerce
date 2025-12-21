using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Carts.Core.Entities;
using YetAnotherECommerce.Modules.Carts.Core.Events;
using YetAnotherECommerce.Modules.Carts.Core.Exceptions;
using YetAnotherECommerce.Modules.Carts.Core.Services;
using YetAnotherECommerce.Shared.Abstractions.Cache;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Carts.UnitTests.Services;

public class CartServiceTests
{
    private readonly Mock<ICache> _cacheMock;
    private readonly Mock<IMessageBroker> _messageBrokerMock;
    private readonly Mock<ILogger<CartService>> _loggerMock;
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        _cacheMock = new Mock<ICache>();
        _messageBrokerMock = new Mock<IMessageBroker>();
        _loggerMock = new Mock<ILogger<CartService>>();
        _cartService = new CartService(_cacheMock.Object, _messageBrokerMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task PlaceOrderAsync_WhenCartIsEmpty_ThenShouldThrowAnException()
    {
        var cart = new Cart();
        var expectedException = new CannotCreateOrderFromEmptyCartException();
        _cacheMock
            .Setup(x => x.Get<Cart>(It.IsAny<string>()))
            .Returns(cart);

        var exception =
            await Assert.ThrowsAsync<CannotCreateOrderFromEmptyCartException>(() => _cartService.PlaceOrderAsync(Guid.NewGuid()));

        expectedException.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<OrderPlaced>()), Times.Never);
    }

    [Fact]
    public async Task PlaceOrderAsync_WhenAtLeastOneItemHasZeroQuantity_ThenShouldThrowAnException()
    {
        var cart = new Cart();
        cart.AddItem(new CartItem(
            productId: Guid.NewGuid(),
            name: "High performance ultrabook",
            quantity: 0,
            unitPrice: 5));
        var expectedException = new CannotOrderProductInZeroQuantityException();
        _cacheMock
            .Setup(x => x.Get<Cart>(It.IsAny<string>()))
            .Returns(cart);
        var exception =
            await Assert.ThrowsAsync<CannotOrderProductInZeroQuantityException>(() => _cartService.PlaceOrderAsync(Guid.NewGuid()));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<OrderPlaced>()), Times.Never);
    }

    [Fact]
    public async Task PlaceOrderAsync_WhenCartItemsAreValidQuantity_ThenShouldPublishIntegrationEvent()
    {
        var cart = new Cart();
        cart.AddItem(new CartItem(
            productId: Guid.NewGuid(),
            name: "High performance ultrabook",
            quantity: 10,
            unitPrice: 5));
        _cacheMock
            .Setup(x => x.Get<Cart>(It.IsAny<string>()))
            .Returns(cart);

        await _cartService.PlaceOrderAsync(Guid.NewGuid());

        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<OrderPlaced>()), Times.Once);
    }
}