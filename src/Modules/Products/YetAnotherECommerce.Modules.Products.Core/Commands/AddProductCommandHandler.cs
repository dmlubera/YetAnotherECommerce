using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<AddProductCommandHandler> _logger;

        public AddProductCommandHandler(IProductRepository productRepository, ILogger<AddProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task HandleAsync(AddProductCommand command)
        {
            if (await _productRepository.CheckIfProductAlreadyExistsAsync(command.Name))
                throw new ProductWithGivenNameAlreadyExistsException();

            var product = new Product(command.Name, command.Description, command.Price, command.Quantity);

            await _productRepository.AddAsync(product);

            _logger.LogInformation("Product added: {@product}", product);
        }
    }
}