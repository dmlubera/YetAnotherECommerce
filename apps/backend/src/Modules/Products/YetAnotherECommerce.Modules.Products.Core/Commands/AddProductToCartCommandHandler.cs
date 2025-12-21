using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Events;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Messages;
using YetAnotherECommerce.Shared.Infrastructure.Messages;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class AddProductToCartCommandHandler : ICommandHandler<AddProductToCartCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<AddProductToCartCommandHandler> _logger;

        public AddProductToCartCommandHandler(IProductRepository productRepository, IMessageBroker messageBroker,
            ILogger<AddProductToCartCommandHandler> logger)
        {
            _productRepository = productRepository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task HandleAsync(AddProductToCartCommand command)
        {
            var product = await _productRepository.GetByIdAsync(command.ProductId);

            if (product is null)
                throw new ProductDoesNotExistException(command.ProductId);

            if (product.Quantity < command.Quantity)
                throw new ProductIsNotAvailableInOrderedQuantityException();

            var productAddedToCart = new ProductAddedToCart(command.CustomerId, command.ProductId,
                product.Name, product.Price, command.Quantity);

            await _messageBroker.PublishAsync(productAddedToCart);

            _logger.LogInformation("Product added to cart: {@product}", productAddedToCart);
        }
    }
}