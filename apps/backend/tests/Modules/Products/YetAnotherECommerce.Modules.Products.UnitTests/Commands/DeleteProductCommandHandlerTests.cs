using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.Repositories;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Commands;

public class DeleteProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock = new();
    private readonly DeleteProductCommandHandler _handler;

    public DeleteProductCommandHandlerTests()
    {
        _handler = new DeleteProductCommandHandler(_productRepositoryMock.Object,
            NullLogger<DeleteProductCommandHandler>.Instance);
    }

    [Fact]
    public async Task WhenValidData_ThenShouldCallDeleteMethodOnRepository()
    {
        var command = new DeleteProductCommand(Guid.NewGuid());

        await _handler.HandleAsync(command);

        _productRepositoryMock.Verify(x => x.DeleteAsync(command.ProductId));
    }
}