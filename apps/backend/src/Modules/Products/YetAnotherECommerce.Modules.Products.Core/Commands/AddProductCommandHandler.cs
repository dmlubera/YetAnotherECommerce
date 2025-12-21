using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands;

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