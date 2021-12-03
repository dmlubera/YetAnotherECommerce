using System.Collections.Generic;
using YetAnotherECommerce.Modules.Products.Core.DTOs;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Products.Core.Queries
{
    public class BrowseProductsQuery : IQuery<IEnumerable<ProductDto>>
    {
    }
}