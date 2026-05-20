using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Catalog.Core.Exceptions;
using YetAnotherECommerce.Modules.Catalog.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Catalog.Core.Commands;

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