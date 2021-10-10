using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class UpdateQuantityCommandHandler : ICommandHandler<UpdateQuantityCommand>
    {
        private readonly IProductRepository _repository;

        public UpdateQuantityCommandHandler(IProductRepository repository)
            => _repository = repository;

        public async Task HandleAsync(UpdateQuantityCommand command)
        {
            var product = await _repository.GetByIdAsync(command.ProductId);

            if (product is null)
                throw new ProductDoesNotExistException(command.ProductId);

            product.UpdateQuantity(command.Quantity);

            await _repository.UpdateAsync(product);
        }
    }
}