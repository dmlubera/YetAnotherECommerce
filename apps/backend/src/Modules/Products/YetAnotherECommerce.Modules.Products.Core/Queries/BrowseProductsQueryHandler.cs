using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Products.Core.DTOs;
using YetAnotherECommerce.Modules.Products.Core.Repositories;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Products.Core.Queries;

public class BrowseProductsQueryHandler(IProductRepository productRepository)
    : IQueryHandler<BrowseProductsQuery, IReadOnlyList<ProductDto>>
{
    public async Task<IReadOnlyList<ProductDto>> HandleAsync(BrowseProductsQuery query)
    {
        var products = await productRepository.GetAsync();

        return products.Select(x => new ProductDto
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            Quantity = x.Quantity
        }).ToList().AsReadOnly();
    }
}