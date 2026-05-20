using System.Collections.Generic;
using YetAnotherECommerce.Modules.Catalog.Core.DTOs;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Modules.Catalog.Core.Queries;

public record BrowseProductsQuery : IQuery<IReadOnlyList<ProductDto>>;