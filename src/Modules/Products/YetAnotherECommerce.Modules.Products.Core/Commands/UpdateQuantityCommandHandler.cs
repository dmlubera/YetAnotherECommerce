using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class UpdateQuantityCommandHandler : ICommandHandler<UpdateQuantityCommand>
    {
        private readonly IProductRepository _repository;

        public UpdateQuantityCommandHandler(IProductRepository repository)
            => _repository = repository;

        public Task HandleAsync(UpdateQuantityCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
