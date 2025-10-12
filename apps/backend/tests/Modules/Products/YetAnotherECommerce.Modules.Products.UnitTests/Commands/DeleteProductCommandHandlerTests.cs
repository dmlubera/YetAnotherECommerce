using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.Repositories;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Commands
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ILogger<DeleteProductCommandHandler>> _loggerMock;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _loggerMock = new Mock<ILogger<DeleteProductCommandHandler>>();
            _handler = new DeleteProductCommandHandler(_productRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task WhenValidData_ThenShouldCallDeleteMethodOnRepository()
        {
            var command = new DeleteProductCommand(Guid.NewGuid());

            await _handler.HandleAsync(command);

            _productRepositoryMock.Verify(x => x.DeleteAsync(command.ProductId));
        }
    }
}