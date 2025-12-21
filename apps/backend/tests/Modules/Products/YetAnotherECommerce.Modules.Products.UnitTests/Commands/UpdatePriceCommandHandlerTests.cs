using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Events;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Commands;

public class UpdatePriceCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IEventDispatcher> _eventDispatcherMock;
    private readonly UpdatePriceCommandHandler _handler;

    public UpdatePriceCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _eventDispatcherMock = new Mock<IEventDispatcher>();
        _handler = new UpdatePriceCommandHandler(_productRepositoryMock.Object, _eventDispatcherMock.Object);
    }

    [Fact]
    public async Task WhenProductWithGivenIdNotExist_ThenShouldThrowAnExceptio()
    {
        var command = new UpdatePriceCommand(Guid.NewGuid(), 10);
        var expectedException = new ProductDoesNotExistException(command.ProductId);
        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        var result = await Assert.ThrowsAsync<ProductDoesNotExistException>(() => _handler.HandleAsync(command));

        result.ShouldNotBeNull();
        result.ErrorCode.ShouldBe(expectedException.ErrorCode);
        result.Message.ShouldBe(expectedException.Message);
    }

    [Fact]
    public async Task WhenGivenDataIsValid_ThenShouldUpdateProductAndSaveChangesToDatabaseAndPublishIntegrationEvent()
    {
        var command = new UpdatePriceCommand(Guid.NewGuid(), 100);
        var product = new Product("Test", string.Empty, 10, 10);
        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);

        await _handler.HandleAsync(command);

        product.Price.Value.ShouldBe(command.Price);
        _productRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Product>()));
        _eventDispatcherMock.Verify(x => x.PublishAsync(It.IsAny<PriceUpdated>()));
    }
}