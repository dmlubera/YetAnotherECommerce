using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Modules.Products.Messages.Events;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class AddProductToCartCommandHandler : ICommandHandler<AddProductToCartCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public AddProductToCartCommandHandler(IProductRepository productRepository, IEventDispatcher eventDispatcher)
        {
            _productRepository = productRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(AddProductToCartCommand command)
        {
            var product = await _productRepository.GetByIdAsync(command.ProductId);

            if (product is null)
                throw new ProductDoesNotExistException(command.ProductId);

            await _eventDispatcher.PublishAsync(new ProductAddedToCart(command.ProductId, command.Quantity));
        }
    }
}