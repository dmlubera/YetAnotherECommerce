using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YetAnotherECommerce.Modules.Catalog.Core.Entitites;
using YetAnotherECommerce.Modules.Catalog.Core.Exceptions;
using YetAnotherECommerce.Modules.Catalog.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Catalog.Core.Commands;

public class AddProductCommandHandler(
    IProductRepository productRepository,
    ILogger<AddProductCommandHandler> logger)
    : ICommandHandler<AddProductCommand>
{
    public async Task HandleAsync(AddProductCommand command)
    {
        if (await productRepository.CheckIfProductAlreadyExistsAsync(command.Name))
            throw new ProductWithGivenNameAlreadyExistsException();

        var product = new Product(command.Name, command.Description, command.Price, command.Quantity);

        await productRepository.AddAsync(product);

        logger.LogInformation("Product added: {@product}", product);
    }
}