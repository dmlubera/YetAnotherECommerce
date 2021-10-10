using Moq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Commands;
using YetAnotherECommerce.Modules.Products.Core.Repositories;

namespace YetAnotherECommerce.Modules.Products.UnitTests.Commands
{
    public class UpdateQuantityCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly UpdateQuantityCommandHandler _handler;

        public UpdateQuantityCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new UpdateQuantityCommandHandler(_productRepositoryMock.Object);
        }

        public async Task WhenGivenQuantityIsLesserThanZero_ThenShouldThrowAnException()
        {

        }

        public async Task WhenProductWithGivenIdNotExist_ThenShouldThrowAnExceptio()
        {

        }

        public async Task WhenGivenDataIsValid_ThenShouldUpdateProductAndSaveChangesToDatabase()
        {

        }
    }
}