using System;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Queries;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Modules.Products.UnitTests.Fixtures.Entities;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Queries;

public class GetProductDetailsQueryHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly GetProductDetailsQueryHandler _handler;

    public GetProductDetailsQueryHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new GetProductDetailsQueryHandler(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task WhenProductWithGivenIdDoesNotExist_ThenShuldThrowAnException()
    {
        var query = new GetProductDetailsQuery(Guid.NewGuid());
        var expectedException = new ProductDoesNotExistException(query.ProductId);
        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        var exception = await Assert.ThrowsAsync<ProductDoesNotExistException>(() => _handler.HandleAsync(query));

        exception.ShouldNotBeNull();
        exception.ErrorCode.ShouldBe(expectedException.ErrorCode);
        exception.Message.ShouldBe(expectedException.Message);
    }

    [Fact]
    public async Task WhenProductWithGivenIdExist_ThenShouldMapToDto()
    {
        var product = ProductFixture.Create();
        var query = new GetProductDetailsQuery(product.Id);
        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);

        var result = await _handler.HandleAsync(query);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(product.Name.Value);
        result.Description.ShouldBe(product.Description);
        result.Price.ShouldBe(product.Price.Value);
        result.Quantity.ShouldBe(product.Quantity.Value);
    }
}