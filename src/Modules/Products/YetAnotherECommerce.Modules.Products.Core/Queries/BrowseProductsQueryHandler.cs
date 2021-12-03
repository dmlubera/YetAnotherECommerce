using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.DTOs;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Products.Core.Queries
{
    public class BrowseProductsQueryHandler : IQueryHandler<BrowseProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public BrowseProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> HandleAsync(BrowseProductsQuery query)
            => _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAsync());
    }
}