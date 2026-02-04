using System;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Commands;

public class UpdatePriceCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly UpdatePriceCommandHandler _handler;

    public UpdatePriceCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        new Mock<IEventDispatcher>();
        _handler = new UpdatePriceCommandHandler(_productRepositoryMock.Object);
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
}