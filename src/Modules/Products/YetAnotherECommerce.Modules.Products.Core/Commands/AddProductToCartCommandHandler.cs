using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Events;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class AddProductToCartCommandHandler : ICommandHandler<AddProductToCartCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMessageBroker _messageBroker;

        public AddProductToCartCommandHandler(IProductRepository productRepository, IMessageBroker messageBroker)
        {
            _productRepository = productRepository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(AddProductToCartCommand command)
        {
            var product = await _productRepository.GetByIdAsync(command.ProductId);

            if (product is null)
                throw new ProductDoesNotExistException(command.ProductId);

            await _messageBroker.PublishAsync(new ProductAddedToCart(command.CustomerId, command.ProductId, product.Name, product.Price, command.Quantity));
        }
    }
}