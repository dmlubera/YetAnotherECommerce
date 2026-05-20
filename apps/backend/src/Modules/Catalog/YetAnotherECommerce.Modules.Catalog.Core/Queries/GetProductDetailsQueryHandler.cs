using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Catalog.Core.DTOs;
using YetAnotherECommerce.Modules.Catalog.Core.Exceptions;
using YetAnotherECommerce.Modules.Catalog.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Catalog.Core.Queries;

public class GetProductDetailsQueryHandler(IProductRepository productRepository)
    : IQueryHandler<GetProductDetailsQuery, ProductDetailsDto>
{
    public async Task<ProductDetailsDto> HandleAsync(GetProductDetailsQuery query)
    {
        var product = await productRepository.GetByIdAsync(query.ProductId);
        if (product is null)
            throw new ProductDoesNotExistException(query.ProductId);

        return new ProductDetailsDto
        {
            Id = product.Id,
            Name = product.Name,
            Description =  product.Description,
            Price = product.Price,
            Quantity =  product.Quantity
        };
    }
}