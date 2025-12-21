using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Modules.Products.UnitTests.Fixtures.Commands;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Commands;

public class AddProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ILogger<AddProductCommandHandler>> _loggerMock;
    private readonly AddProductCommandHandler _handler;

    public AddProductCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _loggerMock = new Mock<ILogger<AddProductCommandHandler>>();
        _handler = new AddProductCommandHandler(_productRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task WhenGivenDataIsCorrect_ThenShouldAddProductToRepository()
    {
        var command = AddProductCommandFixture.Create();
        _productRepositoryMock.Setup(x => x.CheckIfProductAlreadyExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

        await _handler.HandleAsync(command);

        _productRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Product>()));
    }


    [Fact]
    public async Task WhenGivenProductNameIsAlreadyInUse_ThenShouldThrowAnException()
    {
        var command = AddProductCommandFixture.Create();
        var expectedException = new ProductWithGivenNameAlreadyExistsException();
        _productRepositoryMock.Setup(x => x.CheckIfProductAlreadyExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(true);

        var exception =
            await Assert.ThrowsAsync<ProductWithGivenNameAlreadyExistsException>(async () =>
            {
                await _handler.HandleAsync(command);
            });

        exception.Message.ShouldBe(expectedException.Message);
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
    }

    [Fact]
    public async Task WhenGivenProductPriceIsInvalid_ThenShouldThrowAnException()
    {
        var command = AddProductCommandFixture.Create() with { Price = decimal.MinusOne };
        var expectedException = new InvalidPriceException();

        _productRepositoryMock.Setup(x => x.CheckIfProductAlreadyExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

        var exception =
            await Assert.ThrowsAsync<InvalidPriceException>(async () =>
            {
                await _handler.HandleAsync(command);
            });

        exception.Message.ShouldBe(expectedException.Message);
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public async Task WhenGivenProductNameIsInvalid_ThenShouldThrowAnException(string invalidName)
    {
        var command = AddProductCommandFixture.Create() with { Name = invalidName };
        var expectedException = new InvalidProductNameException();

        _productRepositoryMock.Setup(x => x.CheckIfProductAlreadyExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

        var exception =
            await Assert.ThrowsAsync<InvalidProductNameException>(async () =>
            {
                await _handler.HandleAsync(command);
            });

        exception.Message.ShouldBe(expectedException.Message);
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
    }
}