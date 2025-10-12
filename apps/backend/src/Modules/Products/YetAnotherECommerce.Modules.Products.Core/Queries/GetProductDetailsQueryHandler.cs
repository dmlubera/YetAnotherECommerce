using AutoMapper;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.DTOs;
using YetAnotherECommerce.Modules.Products.Core.Exceptions;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Products.Core.Queries
{
    public class GetProductDetailsQueryHandler : IQueryHandler<GetProductDetailsQuery, ProductDetailsDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductDetailsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDetailsDto> HandleAsync(GetProductDetailsQuery query)
        {
            var product = await _productRepository.GetByIdAsync(query.ProductId);
            if (product is null)
                throw new ProductDoesNotExistException(query.ProductId);

            return _mapper.Map<ProductDetailsDto>(product);
        }
    }
}