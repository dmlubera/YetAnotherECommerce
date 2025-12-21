using System;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Commands;

public class UpdateQuantityCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly UpdateQuantityCommandHandler _handler;

    public UpdateQuantityCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new UpdateQuantityCommandHandler(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task WhenGivenQuantityIsLesserThanZero_ThenShouldThrowAnException()
    {
        var command = new UpdateQuantityCommand(Guid.NewGuid(), -1);
        var expectedException = new InvalidQuantityValueException();
        var product = new Product("Test", string.Empty, 10, 10);
        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);

        var result = await Assert.ThrowsAsync<InvalidQuantityValueException>(() => _handler.HandleAsync(command));

        result.ShouldNotBeNull();
        result.ErrorCode.ShouldBe(expectedException.ErrorCode);
        result.Message.ShouldBe(expectedException.Message);
    }

    [Fact]
    public async Task WhenProductWithGivenIdNotExist_ThenShouldThrowAnExceptio()
    {
        var command = new UpdateQuantityCommand(Guid.NewGuid(), -1);
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
    public async Task WhenGivenDataIsValid_ThenShouldUpdateProductAndSaveChangesToDatabase()
    {
        var command = new UpdateQuantityCommand(Guid.NewGuid(), 100);
        var product = new Product("Test", string.Empty, 10, 10);
        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);

        await _handler.HandleAsync(command);

        product.Quantity.Value.ShouldBe(command.Quantity);
        _productRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Product>()));
    }
}