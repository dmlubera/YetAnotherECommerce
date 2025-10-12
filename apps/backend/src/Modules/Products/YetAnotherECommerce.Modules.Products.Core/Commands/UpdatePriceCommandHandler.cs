using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Events;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class UpdatePriceCommandHandler : ICommandHandler<UpdatePriceCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public UpdatePriceCommandHandler(IProductRepository productRepository, IEventDispatcher eventDispatcher)
        {
            _productRepository = productRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(UpdatePriceCommand command)
        {
            var product = await _productRepository.GetByIdAsync(command.ProductId);

            if (product is null)
                throw new ProductDoesNotExistException(command.ProductId);

            product.UpdatePrice(command.Price);

            await _productRepository.UpdateAsync(product);

            await _eventDispatcher.PublishAsync(new PriceUpdated(command.ProductId, product.Price));
        }
    }
}