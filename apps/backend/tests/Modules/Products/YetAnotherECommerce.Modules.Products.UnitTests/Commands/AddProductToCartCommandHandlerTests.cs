using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.Events;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Modules.Products.UnitTests.Fixtures.Entities;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Commands;

public class AddProductToCartCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock = new();
    private readonly Mock<IMessagePublisher> _messageBrokerMock = new();
    private readonly AddProductToCartCommandHandler _handler;

    public AddProductToCartCommandHandlerTests()
    {
        _handler = new AddProductToCartCommandHandler(_productRepositoryMock.Object,
            _messageBrokerMock.Object, NullLogger<AddProductToCartCommandHandler>.Instance);
    }

    [Fact]
    public async Task WhenProductNotExist_ThenShouldThrowAnException()
    {
        var command = CreateCommand();
        var expectedException = new ProductDoesNotExistException(command.ProductId);
        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        var expection = 
            await Assert.ThrowsAsync<ProductDoesNotExistException>(() => _handler.HandleAsync(command));

        expection.ShouldNotBeNull();
        expection.ErrorCode.ShouldBe(expectedException.ErrorCode);
        expection.Message.ShouldBe(expectedException.Message);
        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<ProductAddedToCart>()), Times.Never);
    }

    [Fact]
    public async Task WhenProductIsNotAvailableInOrderedQuantity_ThenShouldThrowAnException()
    {
        var command = CreateCommand();
        var product = ProductFixture.Create();
        product.UpdateQuantity(0);
        var expectedException = new ProductIsNotAvailableInOrderedQuantityException();
        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);

        var exception =
            await Assert.ThrowsAsync<ProductIsNotAvailableInOrderedQuantityException>(() => _handler.HandleAsync(command));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
    }

    [Fact]
    public async Task WhenProductIsAvailable_ThenShouldPublishIntegrationEvent()
    {
        var command = CreateCommand();
        var product = ProductFixture.Create();
        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);

        await _handler.HandleAsync(command);

        _messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<ProductAddedToCart>()));
    }

    private static AddProductToCartCommand CreateCommand()
        => new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            1);
}