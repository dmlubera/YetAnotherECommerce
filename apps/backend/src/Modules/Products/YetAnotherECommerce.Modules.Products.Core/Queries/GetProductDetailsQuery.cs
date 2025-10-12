using System;
using YetAnotherECommerce.Modules.Products.Core.DTOs;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Products.Core.Queries
{
    public class GetProductDetailsQuery : IQuery<ProductDetailsDto>
    {
        public Guid ProductId { get; set; }

        public GetProductDetailsQuery(Guid id)
            => ProductId = id;
    }
}