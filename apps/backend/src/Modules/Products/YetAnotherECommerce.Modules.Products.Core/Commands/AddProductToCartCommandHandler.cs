using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YetAnotherECommerce.Modules.Products.Core.Events;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Modules.Products.Core.Commands;

public class AddProductToCartCommandHandler(
    IProductRepository productRepository,
    IMessagePublisher messagePublisher,
    ILogger<AddProductToCartCommandHandler> logger)
    : ICommandHandler<AddProductToCartCommand>
{
    public async Task HandleAsync(AddProductToCartCommand command)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId);

        if (product is null)
            throw new ProductDoesNotExistException(command.ProductId);

        if (product.Quantity < command.Quantity)
            throw new ProductIsNotAvailableInOrderedQuantityException();

        var productAddedToCart = new ProductAddedToCart(command.CustomerId, command.ProductId,
            product.Name, product.Price, command.Quantity);

        await messagePublisher.PublishAsync(productAddedToCart);

        logger.LogInformation("Product added to cart: {@product}", productAddedToCart);
    }
}