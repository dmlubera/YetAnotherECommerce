using System.Collections.Generic;
using YetAnotherECommerce.Modules.Products.Core.Entitites;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Products.Core.Queries
{
    public class GetAllProductsQuery : IQuery<IEnumerable<Product>>
    {
    }
}