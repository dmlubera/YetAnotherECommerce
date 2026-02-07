using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Carts.Core;
using YetAnotherECommerce.Modules.Carts.Core.Entities;
using YetAnotherECommerce.Modules.Carts.Core.Events;
using YetAnotherECommerce.Modules.Carts.Core.Exceptions;
using YetAnotherECommerce.Modules.Carts.Core.Services;
using YetAnotherECommerce.Shared.Abstractions.Cache;

namespace YetAnotherECommerce.Modules.Carts.UnitTests.Services;

public class CartServiceTests
{
    private readonly Mock<ICache> _cacheMock = new();
    private readonly Mock<ICartsMessagePublisher> _messagePublisherMock = new();
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        _cartService = new CartService(_cacheMock.Object, _messagePublisherMock.Object,
            NullLogger<CartService>.Instance);
    }

    [Fact]
    public async Task PlaceOrderAsync_WhenCartIsEmpty_ThenShouldThrowAnException()
    {
        var cart = new Cart();
        var expectedException = new CannotCreateOrderFromEmptyCartException();
        _cacheMock
            .Setup(x => x.Get(It.IsAny<ICacheKey<Cart>>()))
            .Returns(cart);

        var exception =
            await Assert.ThrowsAsync<CannotCreateOrderFromEmptyCartException>(() => _cartService.PlaceOrderAsync(Guid.NewGuid()));

        expectedException.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
        _messagePublisherMock.Verify(x => x.PublishAsync(It.IsAny<OrderPlaced>()), Times.Never);
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
            .Setup(x => x.Get(It.IsAny<ICacheKey<Cart>>()))
            .Returns(cart);
        var exception =
            await Assert.ThrowsAsync<CannotOrderProductInZeroQuantityException>(() => _cartService.PlaceOrderAsync(Guid.NewGuid()));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
        _messagePublisherMock.Verify(x => x.PublishAsync(It.IsAny<OrderPlaced>()), Times.Never);
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
            .Setup(x => x.Get(It.IsAny<ICacheKey<Cart>>()))
            .Returns(cart);

        await _cartService.PlaceOrderAsync(Guid.NewGuid());

        _messagePublisherMock.Verify(x => x.PublishAsync(It.IsAny<OrderPlaced>()), Times.Once);
    }
}