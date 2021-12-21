using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IProductRepository productRepository,
            ILogger<DeleteProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task HandleAsync(DeleteProductCommand command)
        {
            await _productRepository.DeleteAsync(command.ProductId);

            _logger.LogInformation($"Product with ID: {command.ProductId} has been deleted.");
        }
    }
}