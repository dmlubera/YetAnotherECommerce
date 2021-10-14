using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Products.Core.Queries
{
    public class BrowseProductsQueryHandler : IQueryHandler<BrowseProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public BrowseProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> HandleAsync(BrowseProductsQuery query)
            => await _productRepository.GetAsync();
    }
}