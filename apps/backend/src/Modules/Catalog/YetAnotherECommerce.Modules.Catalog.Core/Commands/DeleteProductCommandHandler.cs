using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YetAnotherECommerce.Modules.Catalog.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Catalog.Core.Commands;

public class DeleteProductCommandHandler(
    IProductRepository productRepository,
    ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task HandleAsync(DeleteProductCommand command)
    {
        await productRepository.DeleteAsync(command.ProductId);

        logger.LogInformation($"Product with ID: {command.ProductId} has been deleted.");
    }
}