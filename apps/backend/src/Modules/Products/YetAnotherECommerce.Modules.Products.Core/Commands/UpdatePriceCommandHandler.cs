using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands;

public class UpdatePriceCommandHandler(IProductRepository productRepository)
    : ICommandHandler<UpdatePriceCommand>
{
    public async Task HandleAsync(UpdatePriceCommand command)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId);

        if (product is null)
            throw new ProductDoesNotExistException(command.ProductId);

        product.UpdatePrice(command.Price);

        await productRepository.UpdateAsync(product);
    }
}