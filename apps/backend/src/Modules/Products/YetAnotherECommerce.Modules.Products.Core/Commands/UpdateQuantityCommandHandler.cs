using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands;

public class UpdateQuantityCommandHandler(IProductRepository repository) : ICommandHandler<UpdateQuantityCommand>
{
    public async Task HandleAsync(UpdateQuantityCommand command)
    {
        var product = await repository.GetByIdAsync(command.ProductId);

        if (product is null)
            throw new ProductDoesNotExistException(command.ProductId);

        product.UpdateQuantity(command.Quantity);

        await repository.UpdateAsync(product);
    }
}