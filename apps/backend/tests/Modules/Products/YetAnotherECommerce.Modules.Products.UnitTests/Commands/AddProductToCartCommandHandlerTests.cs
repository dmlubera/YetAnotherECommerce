using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.Events;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Modules.Products.UnitTests.Fixtures.Entities;
using YetAnotherECommerce.Shared.Abstractions.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Commands;

public class AddProductToCartCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMessageBroker> _messageBrokerMock;
    private readonly Mock<ILogger<AddProductToCartCommandHandler>> _loggerMock;
    private readonly AddProductToCartCommandHandler _handler;

    public AddProductToCartCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _messageBrokerMock = new Mock<IMessageBroker>();
        _loggerMock = new Mock<ILogger<AddProductToCartCommandHandler>>();
        _handler = new AddProductToCartCommandHandler(_productRepositoryMock.Object,
            _messageBrokerMock.Object, _loggerMock.Object);
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
        => new AddProductToCartCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            1);
}